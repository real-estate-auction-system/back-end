using Application.Interfaces;
using Application.ViewModels.RealEstateViewModels;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RealEstateService : IRealEstateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly FirebaseService _firebaseService;
        public RealEstateService(IUnitOfWork unitOfWork, IMapper mapper, FirebaseService firebaseService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _firebaseService = firebaseService;
        }

        public async Task AddAsync(RealEstateModel realEstateModel, int userId)
        {
            var realEstate = _mapper.Map<RealEstate>(realEstateModel);
            if (realEstate == null)
            {
                throw new ArgumentNullException(nameof(realEstate));
            }
            realEstate.AccountId = userId;

            realEstate.AuctionId = 1;
            if (realEstateModel.Image.Count != 0)
            {
                foreach (var singleImage in realEstateModel.Image.Select((image, index) => (image, index)))
                {
                    string newImageName = realEstate.Id + "_i" + singleImage.index;
                    string folderName = $"realEstate/{realEstate.Id}/Image";
                    string imageExtension = Path.GetExtension(singleImage.image.FileName);
                    //Kiểm tra xem có phải là file ảnh không.
                    string[] validImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                    const long maxFileSize = 20 * 1024 * 1024;
                    if (Array.IndexOf(validImageExtensions, imageExtension.ToLower()) == -1 || singleImage.image.Length > maxFileSize)
                    {
                        throw new Exception("Có chứa file không phải ảnh hoặc quá dung lượng tối đa(>20MB)!");
                    }
                    var url = await _firebaseService.UploadFileToFirebaseStorage(singleImage.image, newImageName, folderName);
                    if (url == null)
                        throw new Exception("Lỗi khi đăng ảnh lên firebase!");

                    RealEstateImage realEstateImage = new RealEstateImage()
                    {
                        RealEstateId = realEstate.Id,
                        ImageURL = url
                    };

                    await _unitOfWork.RealEstateImageRepository.AddAsync(realEstateImage);
                }
            }

            await _unitOfWork.RealEstateRepository.AddAsync(realEstate);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteAsync(RealEstate realEstate)
        {
             _unitOfWork.RealEstateRepository.SoftRemove(realEstate);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<List<RealEstate>> GetAll()
        {
            return await _unitOfWork.RealEstateRepository.GetAllAsync();
        }

        public async Task<RealEstate?> GetByIdAsync(int id)
        {
            return await _unitOfWork.RealEstateRepository.GetByIdAsync(id);
        }

        public async Task Update(RealEstate realEstate)
        {
            _unitOfWork.RealEstateRepository.Update(realEstate);
            await _unitOfWork.SaveChangeAsync();
        }

    }
}

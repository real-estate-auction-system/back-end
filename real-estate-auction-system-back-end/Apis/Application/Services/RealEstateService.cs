using Application.Commons;
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
            try
            {
                var realEstate = _mapper.Map<RealEstate>(realEstateModel);
                if (realEstate == null)
                {
                    throw new ArgumentNullException(nameof(realEstate));
                }
                realEstate.IsAvailable = true;
                realEstate.AccountId = userId;
                realEstate.RealEstateStatus = Domain.Enums.RealEstateStatus.notYet;
                realEstate.TypeOfRealEstateId = 1;
                realEstate.AuctionId = 1;
                realEstate.DateSubmited = DateTime.Now.Date;
                await _unitOfWork.RealEstateRepository.AddAsync(realEstate);
                await _unitOfWork.SaveChangeAsync();
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

                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(RealEstate realEstate)
        {
             _unitOfWork.RealEstateRepository.SoftRemove(realEstate);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<List<RealEstate>> GetAll()
        {
            var realEstates = await _unitOfWork.RealEstateRepository.GetAllRealEstates();
            return realEstates;
        }

        public async Task<RealEstate?> GetByIdAsync(int id)
        {
            return await _unitOfWork.RealEstateRepository.GetEstates(id);
        }

        public async Task<Pagination<RealEstate>> GetRealEstateByName(int pageIndex, int pageSize, string name)
        {
            try
            {
                var response = _unitOfWork.RealEstateRepository.FindAll(o => o.Name.Contains(name));
                if (response == null)
                {
                    throw new Exception($"Not found real estate with name {name.ToString()}");
                }
                List<RealEstate> result = new List<RealEstate>();
                foreach (var item in response)
                {
                    result.Add(_mapper.Map<RealEstate>(item));
                }
                var rs = new Pagination<RealEstate>()
                {
                    Items = result,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalItemsCount = result.Count,
                };
                return rs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Get list real estate by type id {name.ToString()} error!");
            }
        }

        public async Task<Pagination<RealEstate>> GetRealEstateByType(int pageIndex, int pageSize, int typeId)
        {
            try
            {
                var response = _unitOfWork.RealEstateRepository.FindAll(o => o.TypeOfRealEstateId == typeId);
                if (response == null)
                {
                    throw new Exception($"Not found real estate with type id {typeId.ToString()}");
                }
                List<RealEstate> result = new List<RealEstate>();
                foreach (var item in response)
                {
                    result.Add(_mapper.Map<RealEstate>(item));
                }
                var rs = new Pagination<RealEstate>()
                {
                    Items = result,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalItemsCount = result.Count,
                };
                return rs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Get list real estate by type id {typeId.ToString()} error!");
            }
        }

        public async Task Update(RealEstate realEstate)
        {
           _unitOfWork.RealEstateRepository.Update(realEstate);
            await _unitOfWork.SaveChangeAsync();          
        }

        public async Task<RealEstate?> UpdateAsync(int id, RealEstateUpdateRequest realEstate)
        {
            var realEstateExisted = await _unitOfWork.RealEstateRepository.GetByIdAsync(id);
            if (realEstateExisted == null)
            {
                return null;
            }
            realEstateExisted.Name = realEstate.Name;
            realEstateExisted.Code = realEstate.Code;
            realEstateExisted.Price = realEstate.Price;
            realEstateExisted.StartPrice = realEstate.StartPrice;
            realEstateExisted.Acreage = realEstate.Acreage;
            realEstateExisted.Address = realEstate.Address;
            realEstateExisted.Province =realEstate.Province;
            realEstateExisted.Description = realEstate.Description;
            realEstateExisted.AuctionId = 1;

            _unitOfWork.RealEstateRepository.Update(realEstateExisted);
            await _unitOfWork.SaveChangeAsync();
            return realEstateExisted;
        }

        public async Task<RealEstate> UpdateRealEstate(RealEstateUpdateRequest request, int Id)
        {
            try
            {
                RealEstate realEstate = await _unitOfWork.RealEstateRepository.FindAsync(o => o.Id == Id);
                if (realEstate == null)
                    throw new Exception($"Not found Real Estate with id {Id.ToString()}");

                var existingRealEstate = _unitOfWork.RealEstateRepository.FindAsync(c => c.Name.Equals(request.Name));
                if (existingRealEstate != null)
                    throw new Exception("Name of Real Estate has already been taken");
                if (request.Price > 0)
                    throw new Exception("Price is invalid");

                _mapper.Map<RealEstateUpdateRequest, RealEstate>(request, realEstate);

                await _unitOfWork.RealEstateRepository.Update(realEstate, Id);
                await _unitOfWork.SaveChangeAsync();
                return realEstate;
            }
            catch (Exception ex)
            {
                throw new Exception("Update Real Estate error!!!!!");
            }

        }
    }
}

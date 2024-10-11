using _Services.Contracts;
using _Services.EntityMapping;
using _Services.Models.Inquiry;
using _Services.Models.Property;
using API_Project.DataAccess.Models;
using Application.DataAccessContracts;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Services.Services
{
    internal class InquiryService : IInquiryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        public InquiryService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public IEnumerable<Inquity_List> GetInquiries()
        {
            try
            {
                var inquiries = _unitOfWork.Inquiry.GetAll().Select(U => new Inquity_List
                {
                    Id = U.Id,
                    UserId = U.UserId,
                    PropertyId = U.PropertyId,
                    UserName = U.User.FullName,
                    PropertyName = U.Property.Title,
                    PhoneNumber = U.PhoneNumber,
                    Message = U.Message,
                    DateSent = U.DateSent
                });

                return inquiries;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while fetching inquiries.", ex);
            }
        }
        public IEnumerable<Inquity_List> GetInquitiesToUser(int UserId)
        {
            try
            {
                var inquiries = _unitOfWork.Inquiry.GetAll().Where(U => U.Property.OwnerId == UserId).Select(U => new Inquity_List
                {
                    Id = U.Id,
                    UserId = U.UserId,
                    PropertyId = U.PropertyId,
                    UserName = U.User.FullName,
                    PropertyName = U.Property.Title,
                    PhoneNumber = U.PhoneNumber,
                    Message = U.Message,
                    DateSent = U.DateSent
                });

                return inquiries;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while fetching inquiries.", ex);
            }
        }

        public IEnumerable<Inquity_List> GetInquitiesToPropety(int propetyId)
        {
            try
            {
                var inquiries = _unitOfWork.Inquiry.GetAll().Where(U => U.PropertyId == propetyId).Select(U => new Inquity_List
                {
                    Id = U.Id,
                    UserId = U.UserId,
                    PropertyId = U.PropertyId,
                    UserName = U.User.FullName,
                    PropertyName = U.Property.Title,
                    PhoneNumber = U.PhoneNumber,
                    Message = U.Message,
                    DateSent = U.DateSent
                });

                return inquiries;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("An error occurred while fetching inquiries.", ex);
            }
        }


        public void CreateInquiry(Inquiry_Create _inquiry)
        {
            try
            {
                ValidateInquiry(_inquiry);

                string phoneNumber = _inquiry.PhoneNumber;

                if (phoneNumber == null)
                    phoneNumber = _userService.GetUserById(_inquiry.UserId).PhoneNumber;

                var inquiry = new Inquiry
                {
                    UserId = _inquiry.UserId,
                    PropertyId = _inquiry.PropertyId,
                    PhoneNumber = phoneNumber,
                    Message = _inquiry.Message,
                    DateSent = DateTime.Now
                };
                _unitOfWork.Inquiry.Insert(inquiry);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("حدث خطأ أثناء إنشاء الاستفسار.", ex);
            }
        }

        public void UpdateInquiry(int Id, Inquiry_Update _inquiry)
        {
            try
            {
                var inquiry = _unitOfWork.Inquiry.GetById(Id);
                if (inquiry == null)
                {
                    throw new KeyNotFoundException("الاستفسار غير موجود.");
                }

                if (_inquiry == null)
                    throw new ApplicationException("الاستفسار لا يمكن أن يكون فارغًا.");
                
                if (!string.IsNullOrWhiteSpace(_inquiry.PhoneNumber))
                    inquiry.PhoneNumber = _inquiry.PhoneNumber;
                if (!string.IsNullOrWhiteSpace(_inquiry.Message))
                    inquiry.Message = _inquiry.Message;
                
                _unitOfWork.Inquiry.Update(inquiry);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("حدث خطأ أثناء تحديث الاستفسار.", ex);
            }
        }

        public void DeleteInquiry(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("معرف الاستفسار غير صحيح.", nameof(id));
            }

            try
            {
                var inquiry = _unitOfWork.Inquiry.GetById(id);
                if (inquiry == null)
                {
                    throw new KeyNotFoundException("الاستفسار غير موجود.");
                }

                _unitOfWork.Inquiry.Delete(id);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("حدث خطأ أثناء حذف الاستفسار.", ex);
            }
        }

        private void ValidateInquiry(Inquiry_Create _inquiry)
        {
            if (_inquiry == null)
                throw new ApplicationException("الاستفسار لا يمكن أن يكون فارغًا.");
            if (_inquiry.UserId <= 0)
                throw new ApplicationException("الرجاء تحديد المستخدم الذي يريد الاستفسار.");
            if (_inquiry.PropertyId <= 0)
                throw new ApplicationException("الرجاء تحديد العقار الذي يريد الاستفسار.");
            var userProperty = _userService.GetUserProperties(_inquiry.UserId).FirstOrDefault(p => p.Id == _inquiry.PropertyId);
            if (userProperty != null)
                throw new ApplicationException("لا يمكن للمستخدم الاستفسار عن عقاره الخاص.");
            
            if (string.IsNullOrWhiteSpace(_inquiry.Message))
                throw new ApplicationException("الرجاء كتابة الرسالة التي يريد الاستفسار.");
        }
    }
}

using _Services.Models.Inquiry;

namespace _Services.Contracts
{
    public interface IInquiryService
    {
        IEnumerable<Inquity_List> GetInquiries();
        IEnumerable<Inquity_List> GetInquitiesToUser(int UserId);
        IEnumerable<Inquity_List> GetInquitiesToPropety(int propetyId);
        void CreateInquiry(Inquiry_Create _inquiry);
        void UpdateInquiry(int Id, Inquiry_Update _inquiry);
        void DeleteInquiry(int id);
    }
}

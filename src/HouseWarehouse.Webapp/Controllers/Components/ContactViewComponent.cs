using HouseWarehouse.Webapp.ApiClient;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers.Components
{
    public class ContactViewComponent : ViewComponent
    {
        private readonly IContactApiClient _contactApiClient;

        public ContactViewComponent(IContactApiClient contactApiClient)
        {
            _contactApiClient = contactApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var banner = await _contactApiClient.GetAll();

            var categories = new List<ContactModel>();
            var data = banner;

            if (data?.Count > 0)
            {
                foreach (var m in data)
                {
                    var item = new ContactModel
                    {
                        ContactId = m.ContactId,
                        Fullname = m.Fullname,
                        Subject = m.Subject,
                        Address = m.Address,
                        Body = m.Body,
                        CreateDate = m.CreateDate,
                        Email = m.Email,
                        Mobile = m.Mobile
                    };
                    categories.Add(item);
                }
            }

            return View(categories);
        }
    }
}
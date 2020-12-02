using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PharmaBackend.DTOs;

namespace PharmaBackend.Helpers
{
    public static class Extensions
    {
        public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage,
            int totalItems, int totalPages)
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }

        public static void FillExpiryDateInDays(this IEnumerable<MedicineDtoForList> medicines)
        {
            foreach (var medicine in medicines)
            {
                if (medicine.ExpiryDate.Date > DateTime.Now.Date)
                {
                    medicine.ExpiringInDays = (int)(medicine.ExpiryDate - DateTime.Now).TotalDays;
                }
            }
        }

        public static void FillExpiryDateInDays(this MedicineDtoForList medicine)
        {
            if (medicine.ExpiryDate.Date > DateTime.Now.Date)
            {
                medicine.ExpiringInDays = (int)(medicine.ExpiryDate - DateTime.Now).TotalDays;
            }
        }
    }
}
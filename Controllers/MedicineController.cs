using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PharmaBackend.DTOs;
using PharmaBackend.Helpers;
using PharmaBackend.Infrastructure;
using PharmaBackend.Models;

namespace PharmaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IPharmaRepository _repo;
        private readonly IMapper _mapper;
        public MedicineController(IPharmaRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }

        [HttpGet(Name = "GetMedicines")]
        public async Task<IActionResult> GetMedicines([FromQuery]SearchParams searchParams)
        {
            var medicines = await _repo.GetMedicines(searchParams);
            
            var medicinesToReturn = _mapper.Map<IEnumerable<MedicineDtoForList>>(medicines);
            medicinesToReturn.FillExpiryDateInDays();

            Response.AddPagination(medicines.CurrentPage, medicines.PageSize, 
                medicines.TotalCount, medicines.TotalPages);
            
            return Ok(medicinesToReturn);
        }

        [HttpGet("{id}", Name = "GetMedicine")]
        public async Task<IActionResult> GetMedicine(int id)
        {
            var medicine = await _repo.GetMedicine(id);

            if (medicine == null)
            {
                return NotFound(); 
            }

            var medicineToReturn = _mapper.Map<MedicineDtoForList>(medicine);

            medicineToReturn.FillExpiryDateInDays();
            return Ok(medicineToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicine(int id, MedicineDtoForUpdate medicineDtoForUpdate)
        {
            var medicineFromRepo = await _repo.GetMedicine(id);

            if (medicineFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(medicineDtoForUpdate, medicineFromRepo);
            
            if(await _repo.SaveAll())
                return NoContent();
            
            throw new Exception($"Updating medicine {id} failed on save.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicine( 
            MedicineDtoForCreation medicineDtoForCreation)
        {   
            var medicine = _mapper.Map<Medicine>(medicineDtoForCreation);
            
            _repo.Add(medicine);

            if(await _repo.SaveAll()) {
                var medicineToReturn = _mapper.Map<MedicineDtoForList>(medicine);
                return CreatedAtRoute("GetMedicine", new {id = medicineToReturn.Id}, medicineToReturn);
            }

            throw new Exception("Creating the message to save.");
        }
    }
}
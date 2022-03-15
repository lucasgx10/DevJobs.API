namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.models;
    using DevJobs.API.persistence;
    using DevJobs.API.persistence.repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Serilog;

    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;
        public JobVacanciesController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var jobVacancies = _repository.GetAll();
            
            return Ok(jobVacancies);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            var jobVacancy = _repository.GetById(id);

            if(jobVacancy == null)
            return NotFound();

            return Ok(jobVacancy);
        }
        /// <summary>
        /// cadastrar uma vaga de emprego.
        /// </summary>
        /// 
        /// <remarks>
        /// {
        /// "title": "dev .net jr",
        /// "description": "vaga para .net jr",
        /// "company": "luizdev",
        /// "isRemote": true,
        /// "salaryRange": "3000-5000"
        /// }
        /// </remarks>
        /// 
        /// <param name="model">dados da vaga.</param>
        /// <returns>objeto recem-criado.</returns>
        /// <response code="201">sucesso.</response>
        

        [HttpPost]
        public IActionResult post(AddJobVacancyInputModel model) {
            Log.Information("POST JobVacancy chamado");
            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );

            _repository.Add(jobVacancy);

            return CreatedAtAction(
                "GetById",
                 new {id = jobVacancy.Id}
                 , jobVacancy);
        }
        [HttpPut ("{id}")]
        public IActionResult Put(int id,UpdateJobVacancyInputModel model){
            var jobVacancy = _repository.GetById(id);

            if(jobVacancy == null)
            return NotFound();

            jobVacancy.Update(model.Title, model.Description);
           
           _repository.Update(jobVacancy);

            return NoContent();
        }


    }
}
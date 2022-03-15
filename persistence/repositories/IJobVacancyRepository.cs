using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevJobs.API.Entities;

namespace DevJobs.API.persistence.repositories
{
    public interface IJobVacancyRepository
    {
        List<JobVacancy> GetAll();

        JobVacancy GetById (int id);

        void Add (JobVacancy jobVacancy);

        void Update (JobVacancy jobVacancy);

        void AddApplication (JobApplication jobApplication);
    }
}
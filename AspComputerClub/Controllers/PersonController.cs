using DataBaseComputerClub;
using Domain.models;
using Interactor;
using Microsoft.AspNetCore.Mvc;


namespace AspComputerClub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {

        DBmonitoring context;

        PersonInteractor interactor;
        public PersonController(DBmonitoring context, PersonInteractor interactor)
        {
            this.interactor = interactor;
            this.context = context;
        }
        [HttpGet("Create")]
        public bool Create(string name, string surname, string patronymic, string login, string password, int comp, bool work)
        {
            return interactor.Create(name, surname, patronymic, login, password, comp, work);
        }
        [HttpPost("Authorization")]
        public int Authorization(string login, string paswword)
        {
            return interactor.Authorization(login, paswword);
        }
        [HttpGet("Work")]
        public bool Work(int idClient)
        {
            return interactor.Work(idClient);
        }
        [HttpGet("NamePerson")]
        public Person NamePerson(int idClient)
        {
            return interactor.NamePerson(idClient);
        }

        [HttpPost("Switch")]
        public int Switch(int id)
        {
            return interactor.Switch(id);
        }

    }
}

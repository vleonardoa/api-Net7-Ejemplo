using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [Route("echoping")]
        public ActionResult<object> EchoPing()
        {
            return Ok(true);
        }


        [HttpGet]
        [Route("ListarRoles")]
        public ActionResult<object> ListarRoles()
        {
            List<DataLayer.EntityModel.ListarRol> ListR = new List<DataLayer.EntityModel.ListarRol>();
            LogicLayer.Logica.Listar list = new LogicLayer.Logica.Listar();

            if (list.ListarRoles(ref ListR))
            {
                return Ok(new
                {
                    ok = true,
                    Response = ListR
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = ListR
                });
            }

        }

        [HttpGet]
        [Route("ListarRolesSP")]
        public ActionResult<object> ListarRolesSP()
        {
            List<DataLayer.EntityModel.ListarRol> ListR = new List<DataLayer.EntityModel.ListarRol>();
            LogicLayer.Logica.Listar list = new LogicLayer.Logica.Listar();

            if (list.ListarRolesSP(ref ListR))
            {
                return Ok(new
                {
                    ok = true,
                    Response = ListR
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = ListR
                });
            }

        }
    }
}
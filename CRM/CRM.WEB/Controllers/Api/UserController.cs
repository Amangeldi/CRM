using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.BLL.Services;
using CRM.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CRM.BLL.DTO;
using CRM.WEB.Models;
using AutoMapper;

namespace CRM.WEB.Controllers.Api
{
    /// <summary>
    /// Контроллер для взаимодействия с пользователем с Пользователем
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserRegistrationService userService;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="serv"></param>
        public UserController(IUserRegistrationService serv)
        {
            userService = serv;
        }
        /// <summary>
        /// Получить авторизованного пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCurrentUser")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            return Ok(userService.GetCurrent(User.Identity.Name));
        }
        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <param name="CreateUserDTO"></param>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        public async Task<ActionResult<CreateUserDTO>> CreateUser(CreateUserDTO CreateUserDTO)
        {
            await userService.CreateUser(CreateUserDTO);
            return Ok(CreateUserDTO);
        }
        /// <summary>
        /// Получить пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<UserViewModel>> GetUser(string id)
        {
            GetUserDTO user = await userService.GetUser(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GetUserDTO, UserViewModel>()
            .ForMember(p=>p.RoleNames, p => p.MapFrom(s=>userService.GetUserRoles(s.Id).Result.ToList()))).CreateMapper();
            UserViewModel userView = mapper.Map<GetUserDTO, UserViewModel>(user);
            return Ok(userView);
        }
        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<GetUserDTO>>> GetUsers()
        {

            return Ok( await userService.GetUsers());
        }
        /// <summary>
        /// Редактировать User-а
        /// </summary>
        /// <param name="GetUserDTO"></param>
        /// <returns></returns>
        [HttpPut("EditUser")]
        public async Task<ActionResult<GetUserDTO>> EditUser(GetUserDTO GetUserDTO)
        {
            await userService.UpdateUser(GetUserDTO);
            return Ok(GetUserDTO);
        }
        /// <summary>
        /// Удалить User-а по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult<GetUserDTO>> DeleteUser(string id)
        {
            await userService.DeleteUser(id);
            return Ok();
        }
    }
}
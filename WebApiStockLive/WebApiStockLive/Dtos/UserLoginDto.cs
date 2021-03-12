﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiStockLive.Dtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Usuário é obrigatório.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória.")]
        public string Password { get; set; }
    }
}

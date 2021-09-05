using System;
using System.Collections.Generic;
using AutoMapper;
using EmployeesAPI.API.DTO;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Models;

namespace EmployeesAPI.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();
        }
    }
}

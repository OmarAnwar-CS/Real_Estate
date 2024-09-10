﻿using AutoMapper;
using application.DataAccess.Models;
using Application.ServiceContracts;
using API_Project.DataAccess.Models;


namespace API_Project.DataAccess.DTOs
{
  
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Property, PropertyDto>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => src.Amenities));
            CreateMap<Property, PropertyDto>();
            CreateMap<PropertyDto, Property>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Inquiry, InquiryDto>();
            CreateMap<InquiryDto, Inquiry>();
            CreateMap<Favorite, FavoriteDto>();
            CreateMap<FavoriteDto, Favorite>();
            CreateMap<Amenities, AmenitiesDto>();
            CreateMap<AmenitiesDto, Amenities>();

        }
    }

}

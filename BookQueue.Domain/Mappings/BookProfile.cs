using AutoMapper;
using BookQueue.Domain.Entities;
using BookQueue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookQueue.Domain.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookModel>()
                .ForMember(dest => dest.AuthorName,
                           opt => opt.MapFrom(src => src.Author.Name));

            CreateMap<BookModel, Book>()
                .ForMember(dest => dest.Author,
                           opt => opt.MapFrom(src => 
                           new Author() 
                           { 
                               Name = src.AuthorName
                           })
                           );
        }
    }
}

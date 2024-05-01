﻿using MediatR;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest<BaseResult>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
    }
}

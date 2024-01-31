using System;
using System.Linq;
using AutoMapper;
using WebApi.Entities;
using WebApi.Utilities.DBOperations;

namespace WebApi.Operations.UserOperations.CreateUsers;

public class CreateUserCommand
{
    readonly IBookStoreDbContext _context;
    readonly IMapper _mapper;

    public CreateUserModel UserModel { get; }

    public CreateUserCommand(IBookStoreDbContext context, IMapper mapper, CreateUserModel userModel)
    {
        _context = context;
        _mapper = mapper;
        UserModel = userModel;
    }

    public void Handle()
    {
        if (_context.Users.Any(x => x.Email.Equals(UserModel.Email))) throw new InvalidOperationException("Email already exists!");
        _context.Users.Add(_mapper.Map<User>(UserModel));
        _context.SaveChanges();
    }

    public class CreateUserModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
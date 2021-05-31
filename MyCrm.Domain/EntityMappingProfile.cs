using AutoMapper;
using MyCrm.Domain.Command.Contact;
using MyCrm.Domain.Command.Order;
using MyCrm.Domain.Command.Role;
using MyCrm.Domain.Command.Todo;
using MyCrm.Domain.Command.User;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Query.Dto;

namespace MyCrm.Domain
{
    public class EntityMappingProfile : Profile
    {
        public void CreateMapForContact()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();

            CreateMap<Contact, AddContactCommand>().ReverseMap();
            CreateMap<ContactDto, AddContactCommand>().ReverseMap();

            CreateMap<Contact, EditContactCommand>().ReverseMap();
            CreateMap<ContactDto, EditContactCommand>().ReverseMap();
        }

        public void CreateMapForOrder()
        {
            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<Order, AddOrderCommand>().ReverseMap();
            CreateMap<OrderDto, AddOrderCommand>().ReverseMap();

            CreateMap<Order, EditOrderCommand>().ReverseMap();
            CreateMap<OrderDto, EditOrderCommand>().ReverseMap();
        }

        public void CreateMapForRole()
        {
            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<Role, AddRoleCommand>().ReverseMap();
            CreateMap<RoleDto, AddRoleCommand>().ReverseMap();

            CreateMap<Role, EditRoleCommand>().ReverseMap();
            CreateMap<RoleDto, EditRoleCommand>().ReverseMap();
        }

        public void CreateMapForTodo()
        {
            CreateMap<Todo, TodoDto>().ReverseMap();

            CreateMap<Todo, AddTodoCommand>().ReverseMap();
            CreateMap<TodoDto, AddTodoCommand>().ReverseMap();

            CreateMap<Todo, EditTodoCommand>().ReverseMap();
            CreateMap<TodoDto, EditTodoCommand>().ReverseMap();
        }

        public void CreateMapForUser()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, AddUserCommand>().ReverseMap();
            CreateMap<UserDto, AddUserCommand>().ReverseMap();

            CreateMap<User, EditUserCommand>().ReverseMap();
            CreateMap<UserDto, EditUserCommand>().ReverseMap();
        }

        public EntityMappingProfile()
        {
            CreateMapForContact();
            CreateMapForOrder();
            CreateMapForRole();
            CreateMapForTodo();
            CreateMapForUser();
        }
    }
}

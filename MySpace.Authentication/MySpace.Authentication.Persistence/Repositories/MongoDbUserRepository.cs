using System.Linq.Expressions;
using AutoMapper;
using MongoDB.Driver;
using MySpace.Authentication.Domain.Configurations;
using MySpace.Authentication.Domain.Exceptions;
using MySpace.Authentication.Domain.Models;
using MySpace.Authentication.Domain.Persistence;
using MySpace.Authentication.Persistence.Configuration;
using MySpace.Authentication.Persistence.Entities;

namespace MySpace.Authentication.Persistence.Repositories;

public class MongoDbUserRepository : UserRepository
{
    private readonly IMongoCollection<UserEntity> _userCollection;
    private readonly IMapper _mapper;
    
    public MongoDbUserRepository(PersistenceConfiguration configuration, IMapper mapper)
    {
        _mapper = mapper;
        _userCollection = new PersistenceFactory(configuration).GetCollection<UserEntity>("users");
    }
    
    public User AddUser(User user)
    {
        _userCollection.InsertOne(_mapper.Map<UserEntity>(user));
        return user;
    }

    public User GetUser(string id)
    {
        ThrowExceptionIfUserNotFound(id);
        var userEntity = _userCollection.Find(MatchUser(id)).First();
        return _mapper.Map<User>(userEntity);
    }

    public User EditUser(string id, User user)
    {
        var originalUser = GetUser(id);
        originalUser.Update(user);
        _userCollection.ReplaceOne(MatchUser(id), _mapper.Map<UserEntity>(originalUser));
        return originalUser;
    }

    public void DeleteUser(string id)
    {
        ThrowExceptionIfUserNotFound(id);
        _userCollection.DeleteOne(MatchUser(id));
    }

    private bool UserExists(string id)
    {
        return _userCollection.CountDocuments(MatchUser(id)) > 0;
    }

    private void ThrowExceptionIfUserNotFound(string id)
    {
        if (!UserExists(id))
        {
            throw new UserNotFoundException(id);
        }
    }

    private static Expression<Func<UserEntity, bool>> MatchUser(string id)
    {
        return u => u.Id == id;
    }
}
using SCSP.Domain.Commons.Request;
using SCSP.Domain.Models;
using SCSP.Infrastructure.Services.Interfaces;
using Npgsql;
using SqlKata;
using SqlKata.Execution;
using SCSP.Domain.Commons.DTO;

namespace SCSP.Infrastructure.Services.Implementations;

public class UserRepository : IUserRepository
{
    private readonly QueryFactory _query;
    private readonly ICryptographyService _cryptographyService;
    private readonly string TableName = "users";

    public UserRepository(IDbConnectionManager connectionManager, ICryptographyService cryptographyService)
    {
        _query = connectionManager.PostgresQueryFactory;
        _cryptographyService = cryptographyService;
    }

    /// <inheritdoc />
    public async Task<bool> CheckedUserByLoginAsync(string login)
    {
        var query = _query.Query(TableName)
            .Where("email", login)
            .Select("email as Email");

        var result = await _query.FirstOrDefaultAsync<string>(query);

        if (result != null) return true;
        else return false;
    }
    
    /// <inheritdoc />
    public async Task<int> CreatedUserAsync(UserModel model)
    {
        string salt = _cryptographyService.GenerateSalt();
        string hashedPassword = _cryptographyService.HashPassword(model.Password, salt);
        model.Password = hashedPassword;
        model.Salt = salt;

        var query = _query.Query(TableName).AsInsert(model);
        
        return await _query.ExecuteAsync(query);
    }
    
    /// <inheritdoc />
    public async Task DeleteUserAsync(string login)
    {
        var query = _query.Query(TableName).Where("email", login).AsDelete();

        await _query.ExecuteAsync(query);
    }

    /// <inheritdoc />
    public async Task<int> DeleteUserAsync(int id)
    {
        var query = _query.Query(TableName).Where("id", id).AsDelete();

        return await _query.ExecuteAsync(query);
    }

    public async Task<IEnumerable<GetRolesDTO>> GetRolesAsync()
    {
        var query = _query.Query("roles").Select("id as Id", "name as Name");

        return await _query.GetAsync<GetRolesDTO>(query);
    }

    /// <inheritdoc />
    public async Task<string?> GetSaltByEmail(string email)
    {
        var query = _query.Query(TableName).Where("email", email).Select("salt");

        var result = await _query.FirstOrDefaultAsync<string?>(query);

        return result;
    }

    public async Task<IEnumerable<GetStudentsDTO>> GetStudentsAsync()
    {
        var query = _query.Query("users as u")
            .LeftJoin("roles as r", "r.id", "u.role_id")
            .Where("r.name", "Ученик")
            .Select("u.id as Id", 
            "u.surname as Surname",
            "u.name as Name",
            "u.patronymic as Patronymic"
            );

        var result = await _query.GetAsync<GetStudentsDTO>(query);

        return result;
    }

    /// <inheritdoc />
    public Task<UserModel> GetUserAsync(string login)
    {
        var query = _query.Query(TableName)
            .Where("email", login)
            .Select("email as Email",
            "password as Password",
            "salt as Salt",
            "created_at as CreatedAt",
            "updated_at as UpdatedAt",
            "is_deleted as IsDeleted");

        var result = _query.FirstOrDefaultAsync<UserModel>(query);
        return result;
    }
    
    /// <inheritdoc />
    public async Task<int> GetUserIdAsync(string login)
    {
        var query = _query.Query(TableName)
            .Where("email", login)
            .Select("id as Id");

        var result = await _query.FirstAsync<int>(query);
        return result;
    }

    public bool IsAdmin(string login)
    {
        var query = _query.Query("users as u")
            .LeftJoin("roles as r", "r.id", "u.role_id")
            .Where("u.email", login)
            .Select("r.name");

        var name = _query.FirstOrDefault<string>(query);

        if (name == "Ученик") return false;
        else return true;
    }

    /// <inheritdoc />
    public async Task<bool> LoginUserAsync(LoginRequest request)
    {
        var query = _query.Query(TableName)
            .Where("email", request.Email)
            .Select("password as Password",
            "salt as Salt");

        if(query == null) return false;
        var result = await _query.FirstOrDefaultAsync<CheckPasswordModel>(query);

        string hash = _cryptographyService.HashPassword(request.Password, result.Salt);

        if (hash == result.Password) return true;
        else return false;
    }
}

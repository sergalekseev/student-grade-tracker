
using StudentGradeTracker.Infra.DataContracts;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace StudentGradeTracker.Services;

internal class ServerApi : IServerApi
{
    private readonly Uri _address;

    public ServerApi()
    {
        _address = new Uri("http://127.0.0.1:6570/api/");
    }

    public async Task<IEnumerable<StudentDto>> GetStudentsAsync()
    {
        // GET + "/api/students"
        using var client = new HttpClient();
        client.BaseAddress = _address;

        var response = await client.GetAsync("students");
        
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var data = await response.Content
            .ReadFromJsonAsync<IEnumerable<StudentDto>>();

        return data;
    }

    public async Task<StudentDto> CreateStudentAsync(StudentCreateDto newStudent)
    {
        // POST + "/api/students"
        using var client = new HttpClient();
        client.BaseAddress = _address;

        var json = JsonSerializer.Serialize(newStudent);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //var content = JsonContent.Create(newStudent);

        var response = await client.PostAsync("students", content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var data = await response.Content
            .ReadFromJsonAsync<StudentDto>();

        return data;
    }

    public async Task<StudentDto> RemoveStudentAsync(string idCard)
    {
        // DELETE + "/api/students"
        using var client = new HttpClient();
        client.BaseAddress = _address;

        var response = await client.DeleteAsync($"students/{idCard}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var data = await response.Content
            .ReadFromJsonAsync<StudentDto>();

        return data;
    }

    public async Task<StudentGradesDto> GetGradesAsync(string idCard)
    {
        // GET + "/api/students/{idCard}/grades"
        using var client = new HttpClient();
        client.BaseAddress = _address;

        var response = await client.GetAsync($"students/{idCard}/grades");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var data = await response.Content
            .ReadFromJsonAsync<StudentGradesDto>();

        return data;
    }
}

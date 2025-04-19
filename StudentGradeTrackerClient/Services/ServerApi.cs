
using StudentGradeTracker.Infra.DataContracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

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
}

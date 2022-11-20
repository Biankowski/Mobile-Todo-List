using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.DataServices
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializeOptions;

        public RestDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5020" : "https://localhost:7260";
            _url = $"{_baseAddress}/api";

            _jsonSerializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        public async Task AddTodoAsync(Todo todo)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet Access");
                return;
            }
            try
            {
                string jsonTodo = JsonSerializer.Serialize<Todo>(todo, _jsonSerializeOptions);
                StringContent content = new StringContent(jsonTodo, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/todo", content);

                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created todo");
                }
                else
                {
                    Debug.WriteLine($"{response.StatusCode}");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e);
            }
            return;

        }

        public async Task DeleteTodoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet Access");
                return;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/todo/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully deleted todo");
                }
                else
                {
                    Debug.WriteLine($"{response.StatusCode}");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine($"{e.Message}");
            }
        }

        public async Task<List<Todo>> GetAllTodosAsync()
        {
            List<Todo> todos = new List<Todo>();

            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet) 
            {
                Debug.WriteLine("No internet Access");
                return todos;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/todo");
                if(response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    todos = JsonSerializer.Deserialize<List<Todo>>(content, _jsonSerializeOptions);
                }
                else
                {
                    Debug.WriteLine($"{response.StatusCode}");
                }
            }
            catch(Exception e) 
            {
                Debug.WriteLine($"{e.Message}");
            }

            return todos;
        }

        public async Task UpdateTodoAsync(Todo todo)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet Access");
                return;
            }
            try
            {
                string jsonTodo = JsonSerializer.Serialize<Todo>(todo, _jsonSerializeOptions);
                StringContent content = new StringContent(jsonTodo, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/todo/{todo.Id}", content);

                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully updated todo");
                }
                else
                {
                    Debug.WriteLine("Error");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
        }
    }
}

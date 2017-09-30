using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TeacherHiring.DataBase.Access;
using TeacherHiring.DataBase.Model;

namespace TeacherHiring
{
	public class TeacherAPI
	{
        const string restURL = "http://online.cuprum.com/webapixamarin/api/{0}";
        HttpClient _client;
        public TeacherAPI()
		{
            _client = new HttpClient();
        }
        public async Task<Usuario> Authenticate(Usuario Usuario)
        {
            var uri = new Uri(string.Format(restURL, "Authenticate/Authenticate/"));
            try
            {
                var json = JsonConvert.SerializeObject(Usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var token = response.Headers.GetValues("Token");
                    var tokenexpiry = response.Headers.GetValues("TokenExpiry");
                    Usuario.Token = token.First();
                    Usuario.TokenExpiry = new DateTime(long.Parse(tokenexpiry.First().ToString()));
                }

                uri = new Uri(string.Format(restURL, "Usuario/GetDataPerson?token=" + Usuario.Token));
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
                UsuarioDataAccess dataAccess = new UsuarioDataAccess();
                message.Headers.Add("Token", Usuario.Token);
                response = await _client.SendAsync(message);
                var result = JsonConvert.DeserializeObject<Usuario>(response.Content.ReadAsStringAsync().Result);
                Usuario.Id = result.Id;
                Usuario.Nombre = result.Nombre;
                Usuario.ClaveAcceso = result.ClaveAcceso;
                Usuario.Contrasena = result.Contrasena;
                Usuario.IdTipoPerson = result.IdTipoPerson;
                Usuario.ClientCreatedOn = result.ClientCreatedOn;
                Usuario.ClientID = result.ClientID;
                Usuario.ClientSecret = result.ClientSecret;
                dataAccess.SaveUser(Usuario);
            }
            catch (WebException e)
            {
                HttpWebResponse response = (HttpWebResponse)e.Response;
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string challenge = null;
                        //challenge = response.GetResponseHeader("WWW-Authenticate");
                        //if (challenge != null)
                        //  Debug.WriteLine("\nThe following challenge was raised by the server:{0}", challenge);
                    }
                    else
                        Debug.WriteLine("\nThe following WebException was raised : {0}", e.Message);
                }
                else
                    Debug.WriteLine("\nResponse Received from server was null");

            }
            catch (Exception e)
            {
                Debug.WriteLine("\nThe following Exception was raised : {0}", e.Message);
            }
            finally
            {
                _client.Dispose();
            }
            return Usuario;
        }
        public async Task<List<Materia>> GetListMaterias(string Token)
        {
            var uri = new Uri(string.Format(restURL, "Materia/GetListMateriaApps"));
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
            MateriaDataAccess dataAccess = new MateriaDataAccess();
            message.Headers.Add("Token", Token);
            HttpResponseMessage response = await _client.SendAsync(message);
            var result = JsonConvert.DeserializeObject < List < Materia>>(response.Content.ReadAsStringAsync().Result);
            List<Materia> materias = new List<Materia>();
            Materia materia = new Materia();
            try
            {
                materias = result;
                foreach (var m in materias)
                {
                    dataAccess.SaveMateria(m);
                }
            }
            catch (WebException e)
            {
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string challenge = null;
                        //challenge = response.GetResponseHeader("WWW-Authenticate");
                        //if (challenge != null)
                        //  Debug.WriteLine("\nThe following challenge was raised by the server:{0}", challenge);
                    }
                    else
                        Debug.WriteLine("\nThe following WebException was raised : {0}", e.Message);
                }
                else
                    Debug.WriteLine("\nResponse Received from server was null");

            }
            catch (Exception e)
            {
                Debug.WriteLine("\nThe following Exception was raised : {0}", e.Message);
            }
            finally
            {
                _client.Dispose();
            }
            return materias;
        }
        public async Task<List<AlumnoMateria>> GetListAlumnoMateria(string Token, int _IdAlumnoMateria)
        {
            var uri = new Uri(string.Format(restURL, "AlumnoMateria/GetListAlumnoMateriaApps?idAlumno="+_IdAlumnoMateria));
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
            AlumnoMateriaDataAccess dataAccess = new AlumnoMateriaDataAccess();
            message.Headers.Add("Token", Token);
            HttpResponseMessage response = await _client.SendAsync(message);
            var result = JsonConvert.DeserializeObject<List<AlumnoMateria>>(response.Content.ReadAsStringAsync().Result);
            List<AlumnoMateria> alumnoMateriaList = new List<AlumnoMateria>();
            try
            {
                alumnoMateriaList = result;
                foreach(var m in alumnoMateriaList)
                {
                    dataAccess.SaveAlumnoMateria(m);
                }
            }
            catch (WebException e)
            {
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string challenge = null;
                        //challenge = response.GetResponseHeader("WWW-Authenticate");
                        //if (challenge != null)
                        //  Debug.WriteLine("\nThe following challenge was raised by the server:{0}", challenge);
                    }
                    else
                        Debug.WriteLine("\nThe following WebException was raised : {0}", e.Message);
                }
                else
                    Debug.WriteLine("\nResponse Received from server was null");

            }
            catch (Exception e)
            {
                Debug.WriteLine("\nThe following Exception was raised : {0}", e.Message);
            }
            finally
            { 
                _client.Dispose();
            }
            return alumnoMateriaList;
        }
        public async Task<List<ProfesorMateria>> GetListProfesorMateria(string Token, int _IdMateria)
        {
            var uri = new Uri(string.Format(restURL, "AlumnoMateria/GetListProfesorMateriaApp?idMateria=" + _IdMateria));
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
            ProfesorMateriaDataAccess dataAccess = new ProfesorMateriaDataAccess();
            message.Headers.Add("Token", Token);
            HttpResponseMessage response = await _client.SendAsync(message);
            var result = JsonConvert.DeserializeObject<List<ProfesorMateria>>(response.Content.ReadAsStringAsync().Result);
            List<ProfesorMateria> profesorMateriaList = new List<ProfesorMateria>();
            try
            {
                profesorMateriaList = result;
                foreach (var m in profesorMateriaList)
                {
                    dataAccess.SaveProfesorMateria(m);
                }
            }
            catch (WebException e)
            {
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string challenge = null;
                        //challenge = response.GetResponseHeader("WWW-Authenticate");
                        //if (challenge != null)
                        //  Debug.WriteLine("\nThe following challenge was raised by the server:{0}", challenge);
                    }
                    else
                        Debug.WriteLine("\nThe following WebException was raised : {0}", e.Message);
                }
                else
                    Debug.WriteLine("\nResponse Received from server was null");

            }
            catch (Exception e)
            {
                Debug.WriteLine("\nThe following Exception was raised : {0}", e.Message);
            }
            finally
            {
                _client.Dispose();
            }
            return profesorMateriaList;
        }
        public async Task<List<AlumnoMateria>> GetListMateriaAceptadaByProfesor(string Token, int _idProfesor)
        {
            var uri = new Uri(string.Format(restURL, "ProfesorMateria/GetListProfesorMateriaApps?idProfesor="+_idProfesor + "&aceptada="+true));
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
            AlumnoMateriaDataAccess dataAccess = new AlumnoMateriaDataAccess();
            message.Headers.Add("Token", Token);
            HttpResponseMessage response = await _client.SendAsync(message);
            var result = JsonConvert.DeserializeObject<List<AlumnoMateria>>(response.Content.ReadAsStringAsync().Result);
            List<AlumnoMateria> alumnoMateriaList = new List<AlumnoMateria>();
            try
            {
                alumnoMateriaList = result;
                foreach (var m in alumnoMateriaList)
                {
                    dataAccess.SaveAlumnoMateria(m);
                }
            }
            catch (WebException e)
            {
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string challenge = null;
                        //challenge = response.GetResponseHeader("WWW-Authenticate");
                        //if (challenge != null)
                        //  Debug.WriteLine("\nThe following challenge was raised by the server:{0}", challenge);
                    }
                    else
                        Debug.WriteLine("\nThe following WebException was raised : {0}", e.Message);
                }
                else
                    Debug.WriteLine("\nResponse Received from server was null");

            }
            catch (Exception e)
            {
                Debug.WriteLine("\nThe following Exception was raised : {0}", e.Message);
            }
            finally
            {
                _client.Dispose();
            }
            return alumnoMateriaList;
        }
    }
}

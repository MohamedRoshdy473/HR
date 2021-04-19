using HR.Domain.Repositories;
using HR.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HR.Core.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly ApplicationDbContext _context;
        private string msg;

        public UniversityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(University University)
        {
            try
            {
                if (University != null)
                {
                    _context.Add(University);
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("University doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }

        public void Delete(int UniversityId)
        {
           var university= _context.Universities.Find(UniversityId);
            try
            {
                if (university != null)
                {
                    _context.Universities.Remove(university);
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("University doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }

        public University Get(int id)
        {
            var university= _context.Universities.Find(id);
            if (university == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("University doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
            else
            {
                return university;

            }
        }

        public IEnumerable<University> GetAll()
        {
           return _context.Universities.ToList();
        }

        public void Update(int UniversityId, University University)
        {
            try
            {
                if (University != null)
                {
                    _context.Entry(University).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("University doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
    }
}

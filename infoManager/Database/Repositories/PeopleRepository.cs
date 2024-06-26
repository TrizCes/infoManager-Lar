﻿using infoManagerAPI.Exceptions;
using infoManagerAPI.Interfaces.Repositories;
using infoManagerAPI.Models;
using infoManagerAPI.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace infoManagerAPI.Data.Repositories
{
    public class PeopleRepository(InfoManagerDbContext context) : IPeopleRepository
    {
        private readonly InfoManagerDbContext _context = context;

        public async Task<bool> CreateAsync(Person person)
        {
            await _context.People.AddAsync(person);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            _context.People.Update(person);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStatus(StatusEnum status, int id)
        {
            var person = await GetByIdAsync(id);
            if (person == null)
            {
                throw new NotFoundException("ID not found");
            }
            else
            {
                person.Status = status;
                _context.Update(person);
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Person>> GetAllAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            var Data = await _context.People.FindAsync(id);
            Detach(Data);
            return Data;
        }

        public async Task<Person?> GetByCpfAsync(string cpf)
        {
            var Data =  await _context.People
               .Where(Person => Person.Cpf == cpf).FirstOrDefaultAsync();
            if(Data != null) Detach(Data);
            return Data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var person = await GetByIdAsync(id);
            _context.People.Remove(person);
            return await _context.SaveChangesAsync() > 0;

        }

        public void Detach(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            EntityEntry<Person> entry = _context.Entry(person);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}

﻿using infoManagerAPI.Interfaces.Repositories;
using infoManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace infoManagerAPI.Data.Repositories
{
    public class PhoneNumbersRepository : IPhoneNumbersRepository
    {
        private readonly InfoManagerDbContext _context;

        public PhoneNumbersRepository(InfoManagerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(PhoneNumber phone)
        {
            await _context.PhoneNumbers.AddAsync(phone);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PhoneNumber> UpdateAsync(PhoneNumber phone)
        {
            _context.PhoneNumbers.Update(phone);
            var sucess = await _context.SaveChangesAsync() > 0;
            if (!sucess) throw new Exception("Failed to update");
            Detach(phone);
            return phone;
        }

        public async Task<List<PhoneNumber>> GetAllAsync()
        {
            return await _context.PhoneNumbers.ToListAsync();
        }

        public async Task<PhoneNumber?> GetByIdAsync(int id)
        {
            var Data = await _context.PhoneNumbers.FindAsync(id);
            Detach(Data);
            return Data;
        }

        public async Task<List<PhoneNumber>> GetByPersonIdAsync(int personId)
        {
            return await _context.PhoneNumbers
              .Where(Phone => Phone.PersonId == personId).ToListAsync();

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await GetByIdAsync(id);
            if (data == null) return false;
            _context.PhoneNumbers.Remove(data);
            return await _context.SaveChangesAsync() > 0;
        }
        public void Detach(PhoneNumber phone)
        {
            if (phone == null)
            {
                throw new ArgumentNullException(nameof(phone));
            }

            EntityEntry<PhoneNumber> entry = _context.Entry(phone);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}

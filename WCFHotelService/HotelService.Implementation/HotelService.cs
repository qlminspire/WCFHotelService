﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using HotelService.Domain;
using HotelService.Data.Abstractions;
using HotelService.Data;

namespace HotelService.Contracts.Implementation
{
    public class HotelService : IHotelService
    {
        private readonly UnitOfWork _repository;

        public HotelService()
        {
            _repository = new UnitOfWork();
        }

        //public HotelService(IHotelRepository repository) { _repository = repository; }

        public Hotel Get(string id)
        {
            Hotel hotel = null;
            if(int.TryParse(id, out int value))
            {
                hotel = _repository.Hotels.Get(value);
            }
            return hotel;
        }

        public IEnumerable<Hotel> GetAll()
        {
            return _repository.Hotels.GetAll();
        }

        public void Create(Hotel hotel)
        {
            _repository.Hotels.Create(hotel);
        }

        public void Update(string id, Hotel hotel)
        {
            var existingHotel = Get(id);
            if(existingHotel != null)
            {
                existingHotel.Address = hotel.Address;
                existingHotel.Rooms = existingHotel.Rooms;
                _repository.Hotels.Update(hotel);
            }
                
        }

        public void Delete(string id)
        {
            var hotel = Get(id);
            if(hotel != null)
                _repository.Hotels.Delete(hotel);
        }

        public IEnumerable<Room> GetHotelRooms(string id)
        {
            IEnumerable<Room> rooms = null;
            if (int.TryParse(id, out int value))
            {
                var hotel = _repository.Hotels.GetHotelWithRooms(value);
                foreach (var room in hotel.Rooms)
                    room.Hotel = null;
                rooms = hotel.Rooms;
            }
            return rooms;
        }

        public IQueryable<Room> GetReservedRooms(string id)
        {
            IQueryable<Room> vacantRooms = null;
            if (int.TryParse(id, out int value))
                vacantRooms = _repository.Hotels.GetReservedRooms(value);
            return vacantRooms;
        }

        public IQueryable<Room> GetVacantRooms(string id)
        {
            IQueryable<Room> vacantRooms = null;
            if(int.TryParse(id, out int value))
                vacantRooms = _repository.Hotels.GetVacantRooms(value);
            return vacantRooms;
        }

        public IQueryable<Room> GetRoomsByType(string id, string type)
        {
            //return _repository.GetRoomsByType(type);
            return null;
        }

        public IQueryable<Room> GetVacantRoomsOfSpecialType(string id, string type)
        {
            //return _repository.GetVacantRoomsOfSpecialType(type);
            return null;
        }

        protected  virtual void Dispose()
        {
            _repository.Dispose();
        }
    }
}

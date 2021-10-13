﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyFly.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EasyFlycomDatabaseEntities : DbContext
    {
        public EasyFlycomDatabaseEntities()
            : base("name=EasyFlycomDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AircraftInfo> AircraftInfoes { get; set; }
        public virtual DbSet<CompanyUserLog> CompanyUserLogs { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<FlightInfo> FlightInfoes { get; set; }
        public virtual DbSet<HotelInfo> HotelInfoes { get; set; }
        public virtual DbSet<SingleUserLog> SingleUserLogs { get; set; }
        public virtual DbSet<CustomerSupport> CustomerSupports { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Membership> Memberships { get; set; }
        public virtual DbSet<PassengerFlight> PassengerFlights { get; set; }
        public virtual DbSet<CargoFlight> CargoFlights { get; set; }
        public virtual DbSet<PackageInfo> PackageInfoes { get; set; }
        public virtual DbSet<BookedPackage> BookedPackages { get; set; }
    }
}
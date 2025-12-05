using System;
using System.Collections.Generic;

namespace HospitalManagementSystem
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public Doctor(int id, string name, string specialization)
        {
            Id = id;
            Name = name;
            Specialization = specialization;
        }
    }

    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Patient(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }

    public class HospitalRoom
    {
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public List<Patient> Patients { get; set; }

        public HospitalRoom(int roomNumber, int capacity)
        {
            RoomNumber = roomNumber;
            Capacity = capacity;
            Patients = new List<Patient>();
        }

        public void AddPatient(Patient patient)
        {
            if (Patients.Count < Capacity)
            {
                Patients.Add(patient);
                Console.WriteLine($"Пацієнт {patient.Name} доданий у палату №{RoomNumber}");
            }
            else
            {
                Console.WriteLine($"Палата №{RoomNumber} переповнена! Неможливо додати пацієнта.");
            }
        }
    }

    public class MedicalRecord
    {
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public MedicalRecord(Patient patient, Doctor doctor, DateTime date, string description)
        {
            Patient = patient;
            Doctor = doctor;
            Date = date;
            Description = description;
        }
    }

    public class Hospital
    {
        public List<Doctor> Doctors { get; set; }
        public List<Patient> Patients { get; set; }
        public List<HospitalRoom> Rooms { get; set; }
        public List<MedicalRecord> Records { get; set; }

        public Hospital()
        {
            Doctors = new List<Doctor>();
            Patients = new List<Patient>();
            Rooms = new List<HospitalRoom>();
            Records = new List<MedicalRecord>();
        }

        public void AddDoctor(Doctor doctor)
        {
            Doctors.Add(doctor);
            Console.WriteLine($"Лікар {doctor.Name} ({doctor.Specialization}) доданий до системи");
        }

        public void RegisterPatient(Patient patient)
        {
            Patients.Add(patient);
            Console.WriteLine($"Пацієнт {patient.Name}, {patient.Age} років, зареєстрований");
        }

        public void CreateRoom(HospitalRoom room)
        {
            Rooms.Add(room);
            Console.WriteLine($"Палата №{room.RoomNumber} створена (місткість: {room.Capacity})");
        }

        public void HospitalizePatient(int patientId, int roomNumber)
        {
            var patient = Patients.Find(p => p.Id == patientId);
            var room = Rooms.Find(r => r.RoomNumber == roomNumber);

            if (patient == null)
            {
                Console.WriteLine($"Пацієнт з ID {patientId} не знайдений!");
                return;
            }

            if (room == null)
            {
                Console.WriteLine($"Палата №{roomNumber} не знайдена!");
                return;
            }

            room.AddPatient(patient);
        }

        public void AddMedicalRecord(MedicalRecord record)
        {
            Records.Add(record);
            Console.WriteLine($"Медичний запис створено: {record.Patient.Name} -> {record.Doctor.Name}");
        }

        public List<MedicalRecord> GetPatientHistory(int patientId)
        {
            return Records.FindAll(r => r.Patient.Id == patientId);
        }

        public string GetStatistics()
        {
            int totalPatientsInRooms = 0;
            foreach (var room in Rooms)
                totalPatientsInRooms += room.Patients.Count;

            return
$@"=== СТАТИСТИКА ЛІКАРНІ ===
Кількість лікарів: {Doctors.Count}
Кількість зареєстрованих пацієнтів: {Patients.Count}
Кількість палат: {Rooms.Count}
Кількість пацієнтів у палатах: {totalPatientsInRooms}
Кількість медичних записів: {Records.Count}
";
        }
    }

    public class HospitalDemo
    {
        public void Run()
        {
            Console.WriteLine("=== СИСТЕМА УПРАВЛІННЯ ЛІКАРНЕЮ ===\n");

            Hospital hospital = new Hospital();

            hospital.AddDoctor(new Doctor(1, "Іванов Іван", "Терапевт"));
            hospital.AddDoctor(new Doctor(2, "Петров Петро", "Хірург"));

            hospital.RegisterPatient(new Patient(1, "Коваленко Тарас", 35));
            hospital.RegisterPatient(new Patient(2, "Шевченко Ольга", 28));
            hospital.RegisterPatient(new Patient(3, "Бондар Андрій", 42));

            hospital.CreateRoom(new HospitalRoom(101, 2));
            hospital.CreateRoom(new HospitalRoom(102, 3));

            hospital.HospitalizePatient(1, 101);
            hospital.HospitalizePatient(2, 101);
            hospital.HospitalizePatient(3, 102);

            hospital.AddMedicalRecord(new MedicalRecord(
                hospital.Patients[0], hospital.Doctors[0], DateTime.Now, "Огляд"));

            hospital.AddMedicalRecord(new MedicalRecord(
                hospital.Patients[0], hospital.Doctors[1], DateTime.Now.AddDays(1), "Повторний огляд"));

            Console.WriteLine("\n--- ІСТОРІЯ ПАЦІЄНТА ---");
            var history = hospital.GetPatientHistory(1);

            foreach (var record in history)
            {
                Console.WriteLine($"  Дата: {record.Date.ToShortDateString()}");
                Console.WriteLine($"  Лікар: {record.Doctor.Name}");
                Console.WriteLine($"  Опис: {record.Description}\n");
            }

            Console.WriteLine(hospital.GetStatistics());
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            HospitalDemo demo = new HospitalDemo();
            demo.Run();

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}

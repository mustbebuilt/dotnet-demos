using System;
using System.Collections.Generic;

public abstract class Employee
{
    private string employeeId;
    private string name;

    public Employee(string employeeId, string name)
    {
        this.employeeId = employeeId;
        this.name = name;
    }

    public string GetEmployeeId() => employeeId;
    public string GetName() => name;

    // Abstract method to be implemented by derived classes
    public abstract double CalculatePay();

    // Support camelCase from specification
    public double calculatePay() => CalculatePay();

    public override string ToString()
    {
        return $"[{employeeId}] {name}";
    }
}

public class SalariedEmployee : Employee
{
    private double annualSalary;

    public SalariedEmployee(string employeeId, string name, double annualSalary)
        : base(employeeId, name)
    {
        this.annualSalary = annualSalary;
    }

    public double GetAnnualSalary() => annualSalary;

    // Calculates monthly salary (annualSalary / 12)
    public override double CalculatePay()
    {
        return annualSalary / 12.0;
    }

    public override string ToString()
    {
        return $"{base.ToString()} (Salaried: £{annualSalary:N2}/yr -> Monthly Pay: £{CalculatePay():N2})";
    }
}

public class HourlyEmployee : Employee
{
    private double hourlyRate;
    private double hoursWorked;

    public HourlyEmployee(string employeeId, string name, double hourlyRate, double hoursWorked)
        : base(employeeId, name)
    {
        this.hourlyRate = hourlyRate;
        this.hoursWorked = hoursWorked;
    }

    public double GetHourlyRate() => hourlyRate;
    public double GetHoursWorked() => hoursWorked;

    // Calculates total pay based on hours worked * hourly rate
    public override double CalculatePay()
    {
        return hourlyRate * hoursWorked;
    }

    public override string ToString()
    {
        return $"{base.ToString()} (Hourly: {hoursWorked} hrs @ £{hourlyRate:N2}/hr -> Pay: £{CalculatePay():N2})";
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("==========================================================");
        Console.WriteLine(" Employee Payroll System - Polymorphism (Exercise 4)");
        Console.WriteLine("==========================================================\n");

        // Create a polymorphic collection of employees
        List<Employee> employees = new List<Employee>
        {
            new SalariedEmployee("E001", "Alice Johnson", 84000.00),
            new HourlyEmployee("E002", "Bob Smith", 25.50, 40.0),
            new SalariedEmployee("E003", "Charlie Davis", 60000.00),
            new HourlyEmployee("E004", "Diana Prince", 35.00, 45.0)
        };

        double totalPayroll = 0;

        Console.WriteLine("Processing Payroll Polymorphically:\n");
        Console.WriteLine($"{"ID",-6} | {"Name",-16} | {"Type",-10} | {"Period Pay",12}");
        Console.WriteLine(new string('-', 52));

        foreach (Employee emp in employees)
        {
            // Polymorphic call to CalculatePay()
            double pay = emp.CalculatePay();
            totalPayroll += pay;

            string empType = emp is SalariedEmployee ? "Salaried" : "Hourly";
            Console.WriteLine($"{emp.GetEmployeeId(),-6} | {emp.GetName(),-16} | {empType,-10} | £{pay,11:N2}");
        }

        Console.WriteLine(new string('-', 52));
        Console.WriteLine($"{"Total Payroll:",-36} | £{totalPayroll,11:N2}");

        Console.WriteLine("\n--- Detailed Employee Info (ToString) ---");
        foreach (Employee emp in employees)
        {
            Console.WriteLine($"• {emp}");
        }

        Console.WriteLine("\n==========================================================");
    }
}

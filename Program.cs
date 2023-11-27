using System;
using System.Collections.Generic;

// Command interface
public interface ICommand
{
    void Execute();
}

// Receiver for Light
public class Light
{
    private bool isOn;

    public void TurnOn()
    {
        isOn = true;
        Console.WriteLine("Light is ON");
    }

    public void TurnOff()
    {
        isOn = false;
        Console.WriteLine("Light is OFF");
    }

    public bool IsOn()
    {
        return isOn;
    }
}

// Receiver for Thermostat
public class Thermostat
{
    private int temperature;

    public void IncreaseTemperature()
    {
        temperature++;
        Console.WriteLine($"Thermostat temperature increased to {temperature}");
    }

    public void DecreaseTemperature()
    {
        temperature--;
        Console.WriteLine($"Thermostat temperature decreased to {temperature}");
    }

    public int GetTemperature()
    {
        return temperature;
    }
}

// Concrete command for turning the light on
public class LightOnCommand : ICommand
{
    private readonly Light light;

    public LightOnCommand(Light light)
    {
        this.light = light;
    }

    public void Execute()
    {
        light.TurnOn();
    }
}

// Concrete command for turning the light off
public class LightOffCommand : ICommand
{
    private readonly Light light;

    public LightOffCommand(Light light)
    {
        this.light = light;
    }

    public void Execute()
    {
        light.TurnOff();
    }
}

// Concrete command for increasing the thermostat temperature
public class ThermostatIncreaseCommand : ICommand
{
    private readonly Thermostat thermostat;

    public ThermostatIncreaseCommand(Thermostat thermostat)
    {
        this.thermostat = thermostat;
    }

    public void Execute()
    {
        thermostat.IncreaseTemperature();
    }
}

// Concrete command for decreasing the thermostat temperature
public class ThermostatDecreaseCommand : ICommand
{
    private readonly Thermostat thermostat;

    public ThermostatDecreaseCommand(Thermostat thermostat)
    {
        this.thermostat = thermostat;
    }

    public void Execute()
    {
        thermostat.DecreaseTemperature();
    }
}

// Composite command for turning the light on/off and increasing/decreasing thermostat temperature
public class CompositeCommand : ICommand
{
    private readonly List<ICommand> commands;

    public CompositeCommand(List<ICommand> commands)
    {
        this.commands = commands;
    }

    public void Execute()
    {
        foreach (var command in commands)
        {
            command.Execute();
        }
    }
}

// Invoker (Remote Controller)
public class RemoteController
{
    private ICommand command;

    public void SetCommand(ICommand command)
    {
        this.command = command;
    }

    public void PressButton()
    {
        command?.Execute();
    }
}

// Client (Main Program)
class Program
{
    static void Main()
    {
        // Create IoT devices
        Light light = new Light();
        Thermostat thermostat = new Thermostat();

        // Create commands
        ICommand lightOn = new LightOnCommand(light);
        ICommand lightOff = new LightOffCommand(light);
        ICommand thermostatIncrease = new ThermostatIncreaseCommand(thermostat);
        ICommand thermostatDecrease = new ThermostatDecreaseCommand(thermostat);

        // Create composite command
        List<ICommand> compositeCommands = new List<ICommand> { lightOn, lightOff, thermostatIncrease, thermostatDecrease };
        ICommand allCommands = new CompositeCommand(compositeCommands);

        // Create remote controller
        RemoteController remoteController = new RemoteController();

        // Set and execute individual commands
        remoteController.SetCommand(lightOn);
        remoteController.PressButton();

        remoteController.SetCommand(thermostatIncrease);
        remoteController.PressButton();

        // Set and execute composite command
        remoteController.SetCommand(allCommands);
        remoteController.PressButton();
    }
}

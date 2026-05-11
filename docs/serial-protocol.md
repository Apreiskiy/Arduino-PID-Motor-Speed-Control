# Serial protocol

Arduino передаёт телеметрию в C# WinForms-приложение по последовательному порту.

## Формат строки телеметрии

```text
T,TimeMs,SetpointRpm,MeasuredRpm,PwmPercent

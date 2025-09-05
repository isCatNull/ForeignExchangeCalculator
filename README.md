# ForeignExchangeCalculator

How do I run it?
Provide args in the following format: <CurrencyExchangeFrom>/<CurrencyExchangeTo> <ammount>
<img width="1230" height="870" alt="image" src="https://github.com/user-attachments/assets/b3d40d35-a54e-4298-86b9-d371bee4096e" />
<img width="842" height="213" alt="image" src="https://github.com/user-attachments/assets/1779351e-5ff5-41f7-8597-ce378acdf6a1" />

Random thoughts...
- Loosely followed Clean Architecture, while keeping it as simple as possible.
- Input validations could be moved out of Program.cs, as it looks a bit bloated.
- No functional requirements were specified for rounding; assumed 4 decimal places based on the sample provided.
- Yen must be a whole number, but online currency exchange calculators often show fractional values.



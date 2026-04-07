# Personal_mortgage_calculator個人房貸試算器1133334

## Personal_mortgage Calculator (WinForms)
This is a desktop application developed using C# Windows Forms, designed to help users accurately calculate various financial indicators related to their mortgage payments. By inputting the home price, interest rate, and grace period, the program automatically calculates the monthly payment amount and total interest expense.關鍵公式貸款總額 ($P$):$$P = \text{房屋總價} \times (1 - \text{自備款}\%)$$每月應繳 (寬限期後):$$\text{Monthly Payment} = P \times \frac{r(1+r)^n}{(1+r)^n-1}$$(其中 $r$ 為月利率，$n$ 為剩餘還款期數)總利息支出:$$\text{Total Interest} = (\text{寬限期利息}) + (\text{剩餘期數本息}) - P$$🖥️ 介面說明項目欄位名稱說明輸入 1房屋總價輸入欲購入房屋的總金額（如 10,000,000）。輸入 2自備款比例以百分比輸入，預設為 20 (即 20%)。輸入 3年利率銀行提供的貸款年利率（如 2.15）。輸入 4貸款年限通常為 20、30 或 40 年。輸入 5寬限期前幾年只繳利息的時間。🛠️ 安裝與執行確認您的電腦已安裝 .NET Framework 執行環境。下載此專案原始碼。使用 Visual Studio 開啟 .sln 專案檔。按下 F5 或點擊「開始」按鈕編譯並運行。
## Features
**diverse input options**:  supporting core parameters such as total house price, down payment ratio, annual interest rate, and loan term. Grace period calculation: fully supports the financial logic of "interest-only payments during the grace period, with principal and interest repaid at maturity.

**Precise financial calculations**:All results are rounded to two decimal places and include thousands separator formatting (N2) for easier readability.

**Input validation**: Built-in error prevention mechanism ensures that the value is valid and greater than zero, preventing program crashes.

**Modern UI**: It adopts rounded corner design and visual highlights to clearly distinguish between the "input area" and the "result area".

## Calculation logic description

1.Total loan amount(P) = Total house price × (1 - Down payment)%

2.Monthly Payment = P x r(1 + r)^r / (1 + r)^n - 1 (Where r is the monthly interest rate, and n is the remaining number of repayment periods)

3.Automatically adjusts text color (Black or White) based on the background's brightness to ensure readability.

## Screenshots

![screenshot](./BMI.png)

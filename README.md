# Cloud Engineer Banking Challenge V1.1
Hi, and welcome to the banking challenge.

# Introduction
Banking is an ancient concept, and it was not always about money. In the very beginning, it was a lot simpler, and one could base it on grains, and other
valuables.

The core idea of banking is that I give you some amount in return for the promise of the same amount, plus some extra as compensation for the service.
For any questions, feel free to reach out to us.

# Challenge
Create a REST API that can generate and return a payment overview for a simplified loan calculation given these parameters:
* Loan amount
* Duration of loan

The bank offers housing loans with the following terms:
* Annual Interest rate: 5%
* Interest rate calculated monthly
* Administration fee (one-time): 1% or 10000 kr. whichever is lowest

The terms should be configurable, but not mandatory.

A payment overview is the most significant details relevant to the loan, such as:
* Effective APR which is the yearly cost as a percentage of the loan amount
* Monthly cost
* Total amount paid in interest rate for the full duration of the loan
* Total amount paid in administrative fees (excl. interest and installments)

# Example
Loan amount of 500.000 kr. for 10 years should show that the monthly payment is 5.303,28 kr.
Furthermore, it shows that the total amount paid as interest rate is 136.393,09 kr. and that administration fee was 5.000 kr.

# Submission Details
Send us a link to the public git repository where you have placed it (e.g. Github), for us to review prior to interview. It will serve as discussion point for the
interview.
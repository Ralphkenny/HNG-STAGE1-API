# HNG12 Stage 1 - Number Classification API

## Description
This is a public API that classifies numbers based on mathematical properties and provides a fun fact about the number.

## Features
- Determines if a number is **prime**.
- Checks if a number is a **perfect number**.
- Identifies if a number is an **Armstrong number**.
- Classifies a number as **odd or even**.
- Computes the **sum of the digits**.
- Fetches a **fun fact** about the number from the [Numbers API](http://numbersapi.com/).
- Returns responses in **JSON format**.

## Technology Stack
- **Language**: C# (.NET 8, ASP.NET Core Web API)
- **Hosting**: Deployed on a publicly accessible endpoint
- **Version Control**: GitHub

## API Documentation
### **Endpoint**
**GET /api/numclass?number={number}**

### **Response Format (200 OK)**
```json
{
    "number": 371,
    "is_prime": false,
    "is_perfect": false,
    "properties": ["armstrong", "odd"],
    "digit_sum": 11,
    "fun_fact": "371 is an Armstrong number because 3^3 + 7^3 + 1^3 = 371"
}
```

### **Response Format (400 Bad Request)**
```json
{
    "number": "invalid_input",
    "error": true
}
```

## Setup Instructions
### **1. Clone the Repository**
```sh
git clone https://github.com/Ralphkenny/HNG-STAGE1-API.git
cd HNG-STAGE1-API

```

### **2. Run the API Locally**
```sh
dotnet run
```

### **3. Test the API**
- Open in browser or Postman:
  ```
  http://localhost:5000/api/numclass?number=371
  ```

## Deployment
This API is hosted on a publicly accessible platform. Access it [here](https://hng-stage1-api.onrender.com/api/numclass?number=371).

## Backlinks

- [Hire C# Developers](https://hng.tech/hire/csharp-developers)

## License
This project is open-source and free to use under the MIT License.

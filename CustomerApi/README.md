# Customer API - Sample JSON Output

## GET /api/customers (first 3 customers from 50 total)

```json
[
  {
    "id": 1,
    "name": "John Smith",
    "email": "john.smith@gmail.com",
    "phone": "(555) 123-4567",
    "city": "New York"
  },
  {
    "id": 2,
    "name": "Emily Johnson",
    "email": "emily.johnson@yahoo.com",
    "phone": "(555) 234-5678",
    "city": "Los Angeles"
  },
  {
    "id": 3,
    "name": "Michael Brown",
    "email": "michael.brown@hotmail.com",
    "phone": "(555) 345-6789",
    "city": "Chicago"
  }
]
```

## API Endpoints

- **GET** `/api/customers` - Returns all 50 customers
- **GET** `/api/customers/{id}` - Returns a single customer by ID
- **POST** `/api/customers` - Creates a new customer
- **PUT** `/api/customers/{id}` - Updates an existing customer
- **DELETE** `/api/customers/{id}` - Deletes a customer

## Example POST Request Body

```json
{
  "name": "Jane Doe",
  "email": "jane.doe@example.com",
  "phone": "(555) 987-6543",
  "city": "Seattle"
}
```

## Example PUT Request Body

```json
{
  "name": "Jane Smith",
  "email": "jane.smith@example.com",
  "phone": "(555) 987-6543",
  "city": "Portland"
}
```
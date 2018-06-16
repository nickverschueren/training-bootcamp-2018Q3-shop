# Node Exercise - shopAPI

Build a REST API to manage a web shop with product list and basket.

1. Add Products API
2. Add integration tests for Product API
3. Add Basket API (with integration tests & without PostMan)
4. Optional: Add Products Paging & Sorting
5. Add MongoDB integration
    * Products
    * Basket
6. Refactor integration tests to use MondoDB
7. Refactor and improve the error messages
8. Refactor to place business logic in separated commands

----

# API

## Summary

GET /api/products
GET /api/products/123
POST /api/products + { ... }
PUT /api/products/123 + { ... }
DELETE /api/products/123

GET /api/basket
POST /api/basket/items/123 + { ... }
PATCH /api/basket/items/123 + { ... }
DELETE /api/basket/items/123
DELETE /api/basket

#### Optional
GET /api/products?page=0&pageSize=5
GET /api/products?sort=+title
PUT /api/products/123/reserve + { ... }

## Products

### GET /api/products

Get all products

*Response*:

    {
        items: [
            {
                "id" : "123",
                "title":"pellentesque",
                "desc":"Donec posuere metus vitae ipsum.",
                "image": 'https://dummyimage.com/300x300.jpg/ff4444/ffffff',
                "price": 16.63,
                "basePrice": 16.63,
                "stock": 10,
                "reserved": 5
            },
            {
                ...
            }
        ],
        total: 10
    }

Status Codes:

* 200: OK

### GET /api/products/123

Get a single product

*Response:*

        {
            "id" : "123",
            "title":"pellentesque",
            "desc":"Donec posuere metus vitae ipsum.",
            "image": 'https://dummyimage.com/300x300.jpg/ff4444/ffffff',
            "price": 16.63,
            "basePrice": 16.63,
            "stock": 10,
            "reserved": 5
        }

*Status Codes:*

* 200: OK
* 404: Not Found

### POST /api/products

Create a new product

*Request with mandatory fields:*

    {
        "title":"pellentesque",
        "basePrice": 16.63,
    }

When price is not provided, the price == basePrice.
When image is not provided, the image == https://dummyimage.com/300x300.jpg
When stock is not provided, the stock == 0
Reserved is always 0.

*Request with all fields:*

    {
        "title": "pellentesque",
        "desc": "Donec posuere metus vitae ipsum.",
        "image": "https://dummyimage.com/300x300.jpg/ff4444/ffffff",
        "price": 16.63,
        "basePrice": 16.63,
        "stock": 1000,
    }

Response:

    {
        "id" : "123",
        "title": "pellentesque",
        "desc": "Donec posuere metus vitae ipsum.",
        "price": 16.63,
        "basePrice": 16.63,
        "stock": 1000,
        "reserved": 0
    }

Response Header:

    Location: http://localhost:3000/api/products/214231535

Status Codes:

* 201: Created
* 400: Bad Request (missing basePrice or title)

### PUT /api/products/123

Update an existing product

*Request with mandatory fields:*

    {
        "title": "pellentesque",
        "basePrice": 16.63,
    }

*Request with all fields:*

    {
        "title": "pellentesque",
        "desc": "Donec posuere metus vitae ipsum.",
        "image": "https://dummyimage.com/300x300.jpg/ff4444/ffffff",
        "price": 16.63,
        "basePrice": 16.63,
        "stock": 10
    }

- When price is not provided, the price == basePrice.
- When image is not provided, the image == https://dummyimage.com/300x300.jpg
- When stock is not provided, the stock == 0

*Response:*

    {
        "id" : "123",
        "title": "pellentesque",
        "desc": "Donec posuere metus vitae ipsum.",
        "price": 16.63,
        "basePrice": 16.63,
        "stock": 10
        "reserved": 10
    }

Status Codes:

* 200: OK
* 400: Bad Request (missing basePrice of title)
* 404: Not Found

### DELETE /api/products/123

Remove an existing product

*Response (the deleted resouce):*

    {
        "id" : "123",
        "title": "pellentesque",
        "desc": "Donec posuere metus vitae ipsum.",
        "price": 16.63,
        "basePrice": 16.63,
        "stock": 10,
        "reserved": 10
    }

Status Codes:

* 200: OK
* 404: Not Found (when the resource was not found)

---

## Basket

### GET /api/basket

Get the basket. Typically we get the basket based on the users identity. For now let fix this to a dummy number: 9001.

*Response:*

    {
        "id" : "998",
        "userIdentity": "9001"
        "items": [
            { productId: "123", quantity: 2 },
            ...
        ],
        lastUpdated: "2014-02-10T10:50:42.389Z"
        created: "2014-02-10T10:50:42.389Z"
    }

*Status Codes:*

* 200: OK
* 404: Not Found (there is no basket for this user)

### POST /api/basket/items/123

Add the product to the basket

*Request:*

    {
        "quantity": 2
    }

*Response:*

    {
        "id" : "998",
        "userIdentity": "9001"
        "items": [
            { productId: "123", quantity: 2 },
            ...
        ]
    }

- When the product already exist the quantity is added.
- When there are not enough products in stock (stock - reserved - quantity <= 0) a 409 is retuned.

*Status Codes:*

* 200: OK
* 400: Bad Request
* 404: Not Found (when the product was not found)
* 409: Conflict (business rule failed: when there are not enough products in stock)

### PATCH /api/basket/items/123

Update the product quantity from the specified basket

*Request:*

    {
        "quantity": 3
    }

*Response:*

    {
        "id" : "998",
        "userIdentity": "9001"
        "items": [
            { productId: "123", quantity: 3 },
            ...
        ]
    }

- When the quantity = 0 the product is removed from the basket. Otherwise it is updated.
- When there are not enough products in stock (stock - reserved - quantity <= 0) a 409 is retuned.

*Status Codes:*

* 200: OK
* 400: Bad Request
* 404: Not Found (when the product was not found on the basket, not basket found or product not found)
* 409: Conflict (business rule failed: when there are not enough products in stock)

### DELETE /api/basket/items/123

Remove a product from the basket

*Response:*

    {
        "id" : "998",
        "userIdentity": "9001"
        "items": [
            { productId: "123", quantity: 12 },
            ...
        ]
    }

Status Codes:

* 200: OK
* 404: Not Found (when the product was not found on the basket, not basket found or product not found)

### DELETE /api/basket

Remove an basket completely

*Response (the deleted resouce):*

    {
        "id" : "998",
        "items": [
            ...
        ]
    }

Status Codes:

* 200: OK
* 404: Not Found (no basket found)

### Optional: PUT /api/products/123/reserve

Reserve a number of products

*Request:*

    {
        "quantity": 3
    }

*Response:*

    {
        "id" : "123",
        "stock": 10
        "reserved": 3
    }

When the quantity > stock a business error should be raised

*Status Codes:*

* 200: OK
* 400: Bad Request
* 409: Failed business rule: out of stock
* 404: Not Found

### Optional: Add product paging

Steps:
1. Create 1000 products with https://www.npmjs.com/package/faker
3. Add paging query params to route

Use Array.slice

    GET /api/users?page=0&pageSize=5

### Optional: Add product sorting

Use: https://www.npmjs.com/package/sort-on

    GET /api/products?sort=+title
    GET /api/products?sort=-price

----

# Connect to DB

## Product db model

    {
        "_id" : ObjectId(...),
        "title":"pellentesque",
        "desc":"Donec posuere metus vitae ipsum.",
        "image": 'https://dummyimage.com/300x300.jpg/ff4444/ffffff',
        "price": 16.63,
        "basePrice": 16.63,
        "stock": {
            "total": 1000,
            "reserved": 5,
        }
    }

## Basket db model

    {
        "_id" : ObjectId(...),      // basket id
        "userIdentity": '9001'      // fix userId for now
        "items": [
            { productId: ObjectId(...), quantity: 10 },
            { productId: ObjectId(...), quantity: 10 }
        },
        lastUpdated: ISODate("2014-02-10T10:50:42.389Z")
        created: ISODate("2014-02-10T10:50:42.389Z")
    }

---

# Improve the error messages

### Global error codes

    200     OK                  Success
    201     Created

    400     Bad Request         The request was invalid or cannot be otherwise
                                served. An accompanying error message will explain details.

    404     Not Found           The URI requested is invalid or the resource
                                requested, such as a user, does not exists.

    409     Conflict            A business rule failed.

    500     Internal Server     Something is broken at the server.
            Error

### Detailed messages

Not Found

    {
        "code": "Not Found",
        "message": "The requested resource was not found"
    }

Bad request

    {
        "code": "Bad Request",
        "message": "One or more input validation is invalid"
        "errors": [
            { "key": "name", "message": "may not be empty"}
            { "key": "email", "message": "not a well-formatted email address"}
        ]
    }

Internal Server Error

    {
        "code": "InternalServerError",
        "message": "Oops! something went wrong!"
        "details": "<detailed error message, only in development>"
    }

Conflict

    {
        "code": "Conflict",
        "message": "One or more business rules failed"
        "errors": [
            { "code": "B010", "message": "Out of stock"}
        ]
    }

General errors:

* When a resource is not found/available: 404 - Not Found
* When a method is not allowed on the resource: 405 - Method Not Allowed
* When the server has an unhandled error/exception: 500 - Internal Server Error


# JSON Models

# Page Template JSON Schema and Connected Property Sample Data

In this readme, you will find examples of JSON schemas representing pages with their associated components and nested structures. 
These schemas demonstrate how to represent the `Page` and `Container` classes described below using JSON format.

## Page 

The following schema defines a single page:

```json
{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "title": "PageTemplate",
  "type": "object",
  "required": ["Id", "Title", "ResourcePath"],
  "properties": {
    "Id": {"type": "integer"},
    "Title": {"type": "string"},
    "ResourcePath": {"type": "string"},
    "PublishStarting": {"format": "date-time"},
    "PublishEnding": {"format": "date-time"},
    "PaddingWidth": {"enum": [1, 2, ..., 8]}
    "Rows": {
      "type": "array",
      "items": {
        "$ref": "#/definitions/Row"
      }
    }
  },
  "definitions": {
    "Row": {
      "type": "object",
      "required": ["Id", "OrderNumber"],
      "properties": {
        "Id": {"type": "integer"},
        "OrderNumber": {"type": "integer"},
        "Columns": {"type": "integer"},
        "Containers": {
          "type": "array",
          "items": {
            "$ref": "#/definitions/Container"
          }
        }
      }
    },
    "Component": {
      "type": "object",
      "required": ["Id", "Name", "Type", "OrderNumber"],
      "properties": {
        "Id": {"type": "integer"},
        "Name": {"type": "string"},
        "Type": {"type": "string"},
        "OrderNumber": {"type": "integer"},
        "Width": {"type": "integer"},
        "Properties": {
          "type": "array",
          "items": {
            "type": "object",
            "properties": {
              "Key": {"type": "string"},
              "Value": {"type": "string"}
            },
            "required": ["Key"]
          }
        }
      }
    }
  }
}
```

## Connected Property Sample Data

To illustrate how these schemas might look in practice, consider the following JSON document which conforms to the schema:

```json
{
  "Id": 1,
  "Title": "My First Page",
  "ResourcePath": "/my-first-page",
  "PublishStarting": "2024-02-16T00:00:00Z",
  "PublishEnding": null,
  "PaddingWidth": 2,
  "Rows": [
    {
          "Id": 2,
          "Name": "Body",
          "Type": "Body",
          "OrderNumber": 2,
          "Width": 12,
          "Properties": [],
          "Containers": [
            {
              "Id": 1,
              "Name": "LeftColumn",
              "Width": 6,
              "Components": [
                {
                  "Id": 1,
                  "Name": "Image",
                  "Type": "Image",
                  "OrderNumber": 1,
                  "Width": 6,
                  "Properties": [
                    {
                      "Key": "ImageSrc",
                      "Value": "https://www.example.com/logo.png"
                    }
                  ]
                },
                {
                  "Id": 2,
                  "Name": "Headline",
                  "Type": "Headline",
                  "OrderNumber": 2,
                  "Width": 6,
                  "Properties": [
                    {
                      "Key": "HeadlineText",
                      "Value": "Welcome to My First Page!"
                    }
                  ]
                },
                {
                  "Id": 3,
                  "Name": "Text",
                  "Type": "Text",
                  "OrderNumber": 3,
                  "Width": 6,
                  "Properties": [
                    {
                      "Key": "Text",
                      "Value": "This is a sample text"    
                    }
                  ]
                }
              ]
            },
            {
              "Id": 2,
              "Name": "RightColumn",
              "Width": 6,
              "Height": 1,
              "Components": [
                {
                  "Id": 1,
                  "Name": "ContactPerson",
                  "Type": "ContactPerson",
                  "OrderNumber": 1,
                  "Width": 6,
                  "Properties": [
                    {
                      "Key": "ContactPersonId",
                      "Value": "00000000-0000-0000-0000-000000000000"
                    },
                    {
                      "Key": "Description",
                      "Value": "AnyDescriptionTextYouLike"
                    }
                  ]
                }
              ]
            }
          ]
        }
      ]
    }
  ]
}
```

This JSON document adheres to the schema specified above while providing a concrete example of how the schemas might be used in practice.

## PageTemplate

```json
{
  "Id": 1,
  "Name": "MyFirstPage",
  "Rows": [
    {
      "Id": "row-example-guid-1",
      "OrderNumber": 1,
      "Containers": [
        {
          "Id": 1,
          "Name": "ExampleContainer1",
          "Width": 8,
          "Height": 1,
          "Components": [
            {
              "Id": 1,
              "Name": "ContactPerson",
              "Type": "ContactPerson",
              "OrderNumber": 1,
              "Width": 6,
              "Properties": [
                {
                  "Key": "ContactPersonId",
                  "Value": "00000000-0000-0000-0000-000000000000"
                },
                {
                  "Key": "Description",
                  "Value": "AnyDescriptionTextYouLike"
                }
              ]
            }
          ]
        }
      ]
    },
    {
      "Id": "row-example-guid-2",
      "OrderNumber": 2,
      "Containers": [
        {
          "Id": 2,
          "Name": "ExampleContainer2",
          "Width": 4,
          "Height": 3,
          "Components": [
            {
              "Id": 2,
              "Name": "EventList",
              "Type": "EventList",
              "OrderNumber": 1,
              "Width": 12,
              "Properties": [],
              "PopulateProperties": () => {} // Method implementation omitted
            },
            {
              "Id": 3,
              "Name": "BlogList",
              "Type": "BlogList",
              "OrderNumber": 2,
              "Width": 12,
              "Properties": [],
              "PopulateProperties": () => {} // Method implementation omitted
            }
          ]
        }
      ]
    }
  ]
}
```

## Row

```json
{
  "Id": "row-example-guid",
  "OrderNumber": 1,
  "Containers": [
    ... // Array of Container objects
  ]
}
```

## Container (Base Class)

```json
{
  "Id": 1,
  "Name": "ExampleContainer",
  "Width": 8,
  "Height": 1,
  "Components": [
    ... // Array of Component objects
  ]
}
```

## Component

```json
{
  "Id": 1,
  "Name": "ContactPerson",
  "Type": "ContactPerson",
  "OrderNumber": 1,
  "Width": 12,
  "Properties": [
    {
      "Key": "EmailAddress",
      "Value": "john@doe.com"
    },
    {
      "Key": "PhoneNumber",
      "Value": "+1234567890"
    }
  ],
  "PopulateProperties": () => {} // Method implementation omitted
}

// Example ComponentType enumeration
enum ComponentType {
  ContactPerson,
  EventList,
  BlogList
}

// Example ComponentProperty object
{
  "Key": "EmailAddress",
  "Value": "john@doe.com"
}
```

Note that the `PopulateProperties` method was added to demonstrate that custom methods could be implemented on the `Component` model if necessary.
export interface Brand {
  BrandId: Number;
  Code: String;
  Name: String;
}

export interface Model {
  ModelId: Number;
  Code: Number;
  Name: String;
  BrandId: Number;
  Brand: {
    BrandId: Number;
    Code: String;
    Name: String;
  };
}

export interface Year {
  YearId: Number;
  Code: String;
  Name: String;
  ModelId: Number;
  Model: {
    ModelId: Number;
    Code: Number;
    Name: String;
    BrandId: Number;
    Brand: null;
  };
}

export interface Price {
  PriceId: Number;
  Value: String;
  BrandName: String;
  ModelName: String;
  ModelYear: Number;
  Fuel: String;
  FipeCode: String;
  ReferenceMonth: String;
  FuelAbbreviation: String;
  BrandId: Number;
  Brand: {
    BrandId: Number;
    Code: String;
    Name: String;
  };
  ModelId: Number;
  Model: {
    ModelId: Number;
    Code: Number;
    Name: String;
    BrandId: Number;
    Brand: {
      BrandId: Number;
      Code: String;
      Name: String;
    };
  };
  YearId: Number;
  Year: {
    YearId: Number;
    Code: String;
    Name: String;
    ModelId: Number;
    Model: null;
  };
}

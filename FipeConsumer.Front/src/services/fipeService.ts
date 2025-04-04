import config from '@/config';
import { Brand, Model, Price, Year } from '@/types';
import axios from 'axios';

const BASE_URL = config.apiBaseUrl;

export const getBrands = async (): Promise<Brand[]> => {
  const response = await axios.get<Brand[]>(`${BASE_URL}Brands`);

  return response.data;
};

export const getModels = async (brandCode: String): Promise<Model[]> => {
  const response = await axios.get<Model[]>(
    `${BASE_URL}Models?brandCode=${brandCode}`
  );

  return response.data;
};

export const getYears = async (modelCode: Number): Promise<Year[]> => {
  const response = await axios.get<Year[]>(
    `${BASE_URL}Years?modelCode=${modelCode}`
  );

  return response.data;
};

export const getPrice = async (
  brandCode: String,
  modelCode: Number,
  yearCode: String
): Promise<Price> => {
  const response = await axios.get<Price>(
    `${BASE_URL}Prices?brandCode=${brandCode}&modelCode=${modelCode}&yearCode=${yearCode}`
  );

  return response.data;
};

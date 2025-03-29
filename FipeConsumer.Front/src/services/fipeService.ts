// src/services/fipeService.ts
import axios from 'axios'
import type { Brand } from '@/types/Brand'  // Você pode definir a interface em um arquivo separado, ex.: src/types/FipeBrand.ts

const API_URL = 'http://localhost:5000/api/fipebrands' // Ajuste conforme necessário

export const getFipeBrands = async (): Promise<Brand[]> => {
  const response = await axios.get<Brand[]>(API_URL)
  return response.data
}

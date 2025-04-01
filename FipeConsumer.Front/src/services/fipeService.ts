import axios from 'axios'
import type { Brand } from '@/types/Brand'

const API_URL = 'http://localhost:5000/api/brands'

export const getFipeBrands = async (): Promise<Brand[]> => {
  const response = await axios.get<Brand[]>(API_URL)
  return response.data
}

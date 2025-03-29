import { ref, computed, onMounted } from 'vue'
import { getFipeBrands } from '@/services/fipeService'
import type { Brand } from '@/types/Brand'

export function useFipeBrands() {
  const brands = ref<Brand[]>([])
  const filter = ref('')

  const fetchBrands = async () => {
    try {
      brands.value = await getFipeBrands()
    } catch (error) {
      console.error('Erro ao buscar as marcas FIPE:', error)
    }
  }

  const filteredBrands = computed(() => {
    if (!filter.value) return brands.value
    return brands.value.filter((brand) =>
      brand.nome.toLowerCase().includes(filter.value.toLowerCase())
    )
  })

  onMounted(() => {
    fetchBrands()
  })

  return { brands, filter, filteredBrands, fetchBrands }
}

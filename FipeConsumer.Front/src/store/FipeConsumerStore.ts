import { defineStore } from 'pinia';
import { ref, computed } from 'vue';
import { Brand, Model, Price, Year } from '@/types';
import {
  getBrands,
  getModels,
  getPrice,
  getYears,
} from '@/services/fipeService';

export const useFipeConsumerStore = defineStore('fipeConsumerStore', () => {
  const brands = ref<Brand[]>([]);
  const selectedBrand = ref<Brand | null>(null);
  const brandFilter = ref('');

  const models = ref<Model[]>([]);
  const selectedModel = ref<Model | null>(null);
  const modelFilter = ref('');

  const years = ref<Year[]>([]);
  const selectedYear = ref<Year | null>(null);
  const yearFilter = ref('');

  const price = ref<Price | null>(null);

  const isLoading = ref<boolean>(false);
  const error = ref<String | null>(null);

  const hasBrands = computed(() => brands.value.length > 0);
  const hasModels = computed(() => models.value.length > 0);
  const hasYears = computed(() => years.value.length > 0);
  const hasPrice = computed(() => price.value !== null);

  const filteredBrands = computed(() => {
    if (!brandFilter.value) return brands.value;
    return brands.value.filter((brand) =>
      brand.Name.toLowerCase().includes(brandFilter.value.toLowerCase())
    );
  });

  const fetchBrands = async () => {
    isLoading.value = true;

    // Simulate response delay
    await new Promise((resolve) => setTimeout(resolve, 500));

    error.value = null;
    try {
      const response = await getBrands();
      brands.value = response;
    } catch (err: any) {
      error.value = err.message || 'Error fetching brands';
    } finally {
      isLoading.value = false;
    }
  };

  const filteredModels = computed(() => {
    if (!modelFilter.value) return models.value;
    return models.value.filter((model) =>
      model.Name.toLowerCase().includes(modelFilter.value.toLowerCase())
    );
  });

  const fetchModels = async (brandCode: String) => {
    isLoading.value = true;

    // Simulate response delay
    await new Promise((resolve) => setTimeout(resolve, 500));

    error.value = null;
    try {
      const response = await getModels(brandCode);
      models.value = response;
    } catch (err: any) {
      error.value = err.message || 'Error fetching models';
    } finally {
      isLoading.value = false;
    }
  };

  const filteredYears = computed(() => {
    if (!yearFilter.value) return years.value;
    return years.value.filter((year) =>
      year.Name.toLowerCase().includes(yearFilter.value.toLowerCase())
    );
  });

  const fetchYears = async (modelCode: Number) => {
    isLoading.value = true;

    // Simulate response delay
    await new Promise((resolve) => setTimeout(resolve, 500));

    error.value = null;
    try {
      const response = await getYears(modelCode);
      years.value = response;
    } catch (err: any) {
      error.value = err.message || 'Error fetching years';
    } finally {
      isLoading.value = false;
    }
  };

  const fetchPrice = async (
    brandCode: String,
    modelCode: Number,
    yearCode: String
  ) => {
    isLoading.value = true;

    // Simulate response delay
    await new Promise((resolve) => setTimeout(resolve, 500));

    error.value = null;
    try {
      const response = await getPrice(brandCode, modelCode, yearCode);
      price.value = response;
    } catch (err: any) {
      error.value = err.message || 'Error fetching vehicle info';
    } finally {
      isLoading.value = false;
    }
  };

  const clearModels = () => {
    models.value = [];
  };
  const clearYears = () => {
    years.value = [];
  };
  const clearPrice = () => {
    price.value = null;
  };
  const clearBrands = () => {
    brands.value = [];
  };

  const handleClearFilters = () => {
    brandFilter.value = '';
    selectedBrand.value = null;
    modelFilter.value = '';
    selectedModel.value = null;
    yearFilter.value = '';
    selectedYear.value = null;

    clearModels();
    clearYears();
    clearPrice();
  };

  return {
    isLoading,
    error,

    brands,
    selectedBrand,
    brandFilter,
    filteredBrands,

    models,
    selectedModel,
    modelFilter,
    filteredModels,

    years,
    selectedYear,
    yearFilter,
    filteredYears,

    price,

    hasBrands,
    hasModels,
    hasYears,
    hasPrice,

    fetchBrands,
    fetchModels,
    fetchYears,
    fetchPrice,

    clearModels,
    clearYears,
    clearPrice,
    clearBrands,
    handleClearFilters,
  };
});

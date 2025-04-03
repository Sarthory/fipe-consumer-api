<script setup lang="ts">
import { useFipeConsumerStore } from '@/store/FipeConsumerStore';
import { Brand } from '@/types';
import { storeToRefs } from 'pinia';

const fipeStore = useFipeConsumerStore();
const { handleClearFilters } = fipeStore;
const { filteredBrands, brandFilter, selectedBrand } = storeToRefs(fipeStore);

const handleSelectBrand = (brand: Brand) => {
  if (brand.BrandId === selectedBrand.value?.BrandId) {
    handleClearFilters();
  } else {
    handleClearFilters();
    selectedBrand.value = brand;
  }
};
</script>

<template>
  <div class="grid">
    <div class="grid__header">
      <v-text-field
        density="compact"
        class="filter"
        v-model="brandFilter"
        clear-icon="mdi-close-circle"
        label="Filter"
        placeholder="Start typing to filter brand names..."
        type="text"
        variant="solo"
        clearable
        @click:clear="() => (brandFilter = '')"
      />

      <div class="selectedItem">
        <span class="selectedItem__label text-body-1">Selected brand:</span>

        <div class="selectedItem__itemName">
          <span>{{ selectedBrand?.Name || 'No brand selected.' }}</span>
          <v-btn
            class="removeButton"
            v-if="selectedBrand"
            density="compact"
            @click="handleClearFilters()"
            color="error"
            icon="mdi-minus"
            title="Unselect brand"
          />
        </div>
      </div>
    </div>

    <div class="grid__table">
      <v-table class="gridElement">
        <tbody>
          <tr
            v-for="brand in filteredBrands"
            :key="`${brand?.Name}`"
            class="tableRow"
            :class="{ selected: selectedBrand?.BrandId === brand.BrandId }"
            @click="handleSelectBrand(brand as Brand)"
          >
            <td class="tableData">
              <div class="gridItem text-body-1">
                {{ brand.Name }}
              </div>
            </td>
          </tr>
          <tr class="tableRow" v-if="!filteredBrands.length">
            <td class="tableData">
              <div class="gridItem">No results found.</div>
            </td>
          </tr>
        </tbody>
      </v-table>
    </div>
  </div>
</template>

<style lang="scss" scoped>
@use './styles/list-styles.scss';
</style>

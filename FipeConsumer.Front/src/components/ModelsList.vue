<script setup lang="ts">
import { useFipeConsumerStore } from '@/store/FipeConsumerStore';
import { Model } from '@/types';
import { storeToRefs } from 'pinia';

const fipeStore = useFipeConsumerStore();
const { filteredModels, modelFilter, selectedModel, yearFilter, selectedYear } =
  storeToRefs(fipeStore);

const handleSelectedModel = (model: Model) => {
  if (model.ModelId === selectedModel.value?.ModelId) {
    handleClearFilters();
  } else {
    handleClearFilters();
    selectedModel.value = model;
  }
};

const handleClearFilters = () => {
  selectedModel.value = null;
  modelFilter.value = '';
  selectedYear.value = null;
  yearFilter.value = '';
};
</script>

<template>
  <div class="grid">
    <div class="grid__header">
      <v-text-field
        density="compact"
        class="filter"
        v-model="modelFilter"
        clear-icon="mdi-close-circle"
        label="Filter"
        placeholder="Start typing to filter model names..."
        type="text"
        variant="solo"
        clearable
        @click:clear="() => (modelFilter = '')"
      />

      <div class="selectedItem">
        <span class="selectedItem__label text-body-1">Selected model:</span>

        <div class="selectedItem__itemName">
          <span>{{ selectedModel?.Name || 'No model selected.' }}</span>
          <v-btn
            class="removeButton"
            v-if="selectedModel"
            density="compact"
            @click="handleClearFilters()"
            color="error"
            icon="mdi-minus"
            title="Unselect model"
          />
        </div>
      </div>
    </div>

    <div class="grid__table">
      <v-table class="gridElement">
        <tbody>
          <tr
            v-for="model in filteredModels"
            :key="`${model?.Name}`"
            class="tableRow"
            :class="{ selected: selectedModel?.ModelId === model.ModelId }"
            @click="handleSelectedModel(model as Model)"
          >
            <td class="tableData">
              <div class="gridItem text-body-1">
                {{ model.Name }}
              </div>
            </td>
          </tr>
          <tr class="tableRow" v-if="!filteredModels.length">
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

<script setup lang="ts">
import { useFipeConsumerStore } from '@/store/FipeConsumerStore';
import { Model } from '@/types';
import { scrollItemIntoView } from '@/utils';
import { storeToRefs } from 'pinia';
import { nextTick } from 'vue';

const fipeStore = useFipeConsumerStore();
const { filteredModels, modelFilter, selectedModel, yearFilter, selectedYear } =
  storeToRefs(fipeStore);

const handleSelectedModel = (model: Model) => {
  if (model.ModelId === selectedModel.value?.ModelId) {
    handleClearFilters();
  } else {
    handleClearFilters();
    selectedModel.value = model;
    nextTick(() => {
      scrollItemIntoView(`list-item-${model.ModelId}`);
    });
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
        append-inner-icon="mdi-magnify"
        @click:clear="() => (modelFilter = '')"
      />

      <div class="selectedItem">
        <span class="selectedItem__label text-body-1">
          <v-icon class="icon" icon="mdi-car" />
          <span>Selected model:</span>
        </span>

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
            :id="`list-item-${model?.ModelId}`"
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

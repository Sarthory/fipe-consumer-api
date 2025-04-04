<script setup lang="ts">
import { useFipeConsumerStore } from '@/store/FipeConsumerStore';
import { Year } from '@/types';
import { scrollItemIntoView } from '@/utils';
import { storeToRefs } from 'pinia';
import { nextTick } from 'vue';

const fipeStore = useFipeConsumerStore();
const { filteredYears, yearFilter, selectedYear, price } =
  storeToRefs(fipeStore);

const handleSelectedYear = (year: Year) => {
  if (year.YearId === selectedYear.value?.YearId) {
    handleClearFilters();
  } else {
    handleClearFilters();
    selectedYear.value = year;
    nextTick(() => {
      scrollItemIntoView(`list-item-${year.YearId}`);
    });
  }
};

const handleClearFilters = () => {
  selectedYear.value = null;
  yearFilter.value = '';
  price.value = null;
};
</script>

<template>
  <div class="grid">
    <div class="grid__header">
      <v-text-field
        density="compact"
        class="filter"
        v-model="yearFilter"
        clear-icon="mdi-close-circle"
        label="Filter"
        placeholder="Start typing to filter model years..."
        type="text"
        variant="solo"
        append-inner-icon="mdi-magnify"
        clearable
        @click:clear="() => (yearFilter = '')"
      />

      <div class="selectedItem">
        <span class="selectedItem__label text-body-1">
          <v-icon class="icon" icon="mdi-calendar-star-four-points" />
          <span>Selected year:</span>
        </span>

        <div class="selectedItem__itemName">
          <span>{{ selectedYear?.Name || 'No year selected.' }}</span>
          <v-btn
            class="removeButton"
            v-if="selectedYear"
            density="compact"
            @click="handleClearFilters()"
            color="error"
            icon="mdi-minus"
            title="Unselect year"
          />
        </div>
      </div>
    </div>

    <div class="grid__table">
      <v-table class="gridElement">
        <tbody>
          <tr
            v-for="year in filteredYears"
            :key="`${year?.Name}`"
            class="tableRow"
            :class="{ selected: selectedYear?.YearId === year.YearId }"
            @click="handleSelectedYear(year as Year)"
            :id="`list-item-${year?.YearId}`"
          >
            <td class="tableData">
              <div class="gridItem text-body-1">
                {{ year.Name }}
              </div>
            </td>
          </tr>
          <tr class="tableRow" v-if="!filteredYears.length">
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

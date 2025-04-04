<script setup lang="ts">
import { nextTick, onBeforeMount, ref } from 'vue';
import BrandsList from '@/components/BrandsList.vue';
import {
  VStepperVertical,
  VStepperVerticalItem,
} from 'vuetify/labs/VStepperVertical';
import { useFipeConsumerStore } from '@/store/FipeConsumerStore';
import ModelsList from '@/components/ModelsList.vue';
import { storeToRefs } from 'pinia';
import YearsList from '@/components/YearsList.vue';
import PriceCard from '@/components/PriceCard.vue';
import { Steps } from '@/enums/steps';
import { scrollItemIntoView } from '@/utils';

const fipeStore = useFipeConsumerStore();
const {
  clearModels,
  clearYears,
  clearPrice,
  fetchBrands,
  fetchModels,
  fetchYears,
  fetchPrice,
  handleClearFilters,
} = fipeStore;
const { selectedBrand, selectedModel, selectedYear, isLoading, price } =
  storeToRefs(fipeStore);

onBeforeMount(() => {
  fetchBrands();
});

const currentStep = ref<Steps>(Steps.Brand);

const goToStep = async (step: Steps) => {
  let listItemId: String;

  switch (step) {
    case Steps.Brand: {
      clearModels;
      clearYears;
      clearPrice;

      if (selectedBrand.value !== null)
        listItemId = `list-item-${selectedBrand.value.BrandId}`;

      break;
    }

    case Steps.Model: {
      clearModels;
      clearYears;
      clearPrice;
      await fetchModels(selectedBrand.value.Code);

      if (selectedModel.value !== null)
        listItemId = `list-item-${selectedModel.value.ModelId}`;
      break;
    }

    case Steps.Year: {
      clearYears;
      clearPrice;
      await fetchYears(selectedModel.value.Code);

      if (selectedYear.value !== null)
        listItemId = `list-item-${selectedYear.value.YearId}`;

      break;
    }

    case Steps.Price: {
      clearPrice;
      await fetchPrice(
        selectedBrand.value.Code,
        selectedModel.value.Code,
        selectedYear.value.Code
      );
      break;
    }
  }

  currentStep.value = step;

  nextTick(() => {
    scrollItemIntoView(listItemId);
  });
};

function resetStepper() {
  handleClearFilters();
  fetchBrands();
  currentStep.value = Steps.Brand;
}
</script>

<template>
  <div>
    <v-stepper-vertical v-model="currentStep">
      <template v-slot:default>
        <!-- Step 1 -->
        <v-stepper-vertical-item
          :subtitle="
            selectedBrand
              ? `${selectedBrand.Name}`
              : 'Select a car brand to acquire the available models'
          "
          title="Brand"
          editable
          ripple
          @click="
            () =>
              selectedBrand &&
              scrollItemIntoView(`list-item-${selectedBrand.BrandId}`)
          "
          :value="Steps.Brand"
        >
          <!-- List element -->
          <BrandsList />

          <template v-slot:next>
            <v-btn
              elevation="2"
              :loading="isLoading"
              color="primary"
              @click="goToStep(Steps.Model)"
              :disabled="Boolean(selectedBrand === null)"
            >
              Next
            </v-btn>
          </template>

          <template v-slot:prev />
        </v-stepper-vertical-item>

        <!-- Step 2 -->
        <v-stepper-vertical-item
          :editable="
            Boolean(selectedModel !== null) && Boolean(selectedBrand !== null)
          "
          :subtitle="
            selectedModel
              ? `${selectedModel.Name}`
              : 'Select a car model to acquire the fabrication years'
          "
          title="Model"
          :value="Steps.Model"
          ripple
          @click="
            () =>
              selectedModel &&
              scrollItemIntoView(`list-item-${selectedModel.ModelId}`)
          "
        >
          <!-- List element -->
          <ModelsList />

          <template v-slot:next>
            <v-btn
              color="primary"
              elevation="2"
              :loading="isLoading"
              @click="goToStep(Steps.Year)"
              :disabled="
                Boolean(selectedBrand !== null) &&
                Boolean(selectedModel === null)
              "
            >
              Next
            </v-btn>
          </template>

          <template v-slot:prev>
            <v-btn variant="plain" @click="goToStep(Steps.Brand)">
              Previous
            </v-btn>
          </template>
        </v-stepper-vertical-item>

        <!-- Step 3 -->
        <v-stepper-vertical-item
          :subtitle="
            selectedYear
              ? `${selectedYear.Name}`
              : 'Select a fabrication year to acquire the vahicle information'
          "
          title="Year / Fuel"
          :editable="
            Boolean(selectedYear !== null) &&
            Boolean(selectedModel !== null) &&
            Boolean(selectedBrand !== null)
          "
          :value="Steps.Year"
          ripple
          @click="
            () =>
              selectedYear &&
              scrollItemIntoView(`list-item-${selectedYear.YearId}`)
          "
        >
          <!-- List element -->
          <YearsList />

          <template v-slot:next>
            <v-btn
              elevation="2"
              color="primary"
              :loading="isLoading"
              @click="goToStep(Steps.Price)"
              :disabled="
                Boolean(selectedYear === null) &&
                Boolean(selectedModel !== null) &&
                Boolean(selectedBrand !== null)
              "
            >
              Next
            </v-btn>
          </template>

          <template v-slot:prev>
            <v-btn variant="plain" @click="goToStep(Steps.Model)">
              Previous
            </v-btn>
          </template>
        </v-stepper-vertical-item>

        <!-- Step 4 -->
        <v-stepper-vertical-item
          title="Price"
          :editable="
            Boolean(selectedYear !== null) &&
            Boolean(selectedModel !== null) &&
            Boolean(selectedBrand !== null) &&
            Boolean(price !== null)
          "
          ripple
          :value="Steps.Price"
        >
          <!-- Result element -->
          <PriceCard />

          <template v-slot:next />

          <template v-slot:prev>
            <v-btn variant="plain" @click="goToStep(Steps.Year)">
              Previous
            </v-btn>

            <v-btn
              text="Reset"
              color="warning"
              variant="elevated"
              @click="resetStepper"
            >
              Start over
            </v-btn>
          </template>
        </v-stepper-vertical-item>
      </template>
    </v-stepper-vertical>
  </div>
</template>

<style scoped></style>

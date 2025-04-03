<template>
  <div class="vehicePrice">
    <v-card elevation="4" class="vehicePrice__card">
      <v-card variant="tonal" class="price">
        <span class="text-h4"> {{ price.Value }} </span>
      </v-card>

      <span class="item">
        Brand: <strong>{{ price.BrandName }}</strong>
      </span>
      <span class="item">
        Model: <strong>{{ price.ModelName }}</strong>
      </span>
      <span class="item">
        Model year: <strong>{{ price.ModelYear }}</strong>
      </span>
      <span class="item">
        Fuel: <strong>{{ price.Fuel }}</strong>
      </span>
      <span class="item fipe">
        <span>
          Fipe code: <strong class="code">{{ price.FipeCode }}</strong>
        </span>
        <span class="reference">
          Reference month: <strong>{{ price.ReferenceMonth }}</strong>
        </span>
      </span>
    </v-card>
  </div>
</template>

<script setup>
import { useFipeConsumerStore } from '@/store/FipeConsumerStore';
import { storeToRefs } from 'pinia';

const fipeStore = useFipeConsumerStore();
const { price } = storeToRefs(fipeStore);
</script>

<style lang="scss" scoped>
.vehicePrice {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;

  &__card {
    display: flex;
    align-items: flex-start;
    justify-content: center;
    gap: 10px;
    flex-flow: column;
    padding: 15px;
    width: 100%;
    background-color: #eee;

    .price {
      width: 100%;
      padding: 8px;
      background-color: lightgreen;
      margin-bottom: 10px;

      > span {
        color: forestgreen;
        font-weight: bold;
      }
    }

    .item {
      width: 100%;
      color: #444;

      &.fipe {
        margin-top: 10px;
        font-size: 14px;
        color: #666;
        display: flex;
        flex-flow: column;
        align-items: flex-end;

        .code {
          color: #2167c0;
        }

        .reference {
          font-size: 10px;
        }
      }
    }
  }
}
</style>

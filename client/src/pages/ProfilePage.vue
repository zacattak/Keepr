<template>
  <section class="container-fluid">



    <div v-if="profile != null">

      <!-- <div class=""> -->
      <img class="banner-img" :src="profile.coverImg" alt="">
      <!-- </div> -->

      <div class="d-flex justify-content-center">

        <h1>Welcome to {{ profile.name }}'s profile!</h1>

      </div>

      <div class="text-center">
        <img class="rounded image" :src="profile.picture" alt="" />
      </div>

    </div>


    <div class="row">
      <h1 class="col-12 text-center">Keeps</h1>

      <div class="masonry">

        <div v-for="keep in keeps" :key="keep.id" class="">
          <KeepComponent :keep="keep" />
        </div>

      </div>

    </div>
    <div class="row">
      <h1 class="col-12 text-center">Vaults</h1>
      <div class="masonry">
        <div v-for="vault in vaults" :key="vault.id" class="">
          <VaultComponent :vault="vault" />
        </div>
      </div>
    </div>

  </section>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import { useRoute } from 'vue-router';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop.js';
import { accountService } from '../services/AccountService.js';
import { vaultsService } from '../services/VaultsService.js';
import { keepsService } from '../services/KeepsService.js';

export default {
  setup() {
    const route = useRoute();
    async function getAccountById() {
      try {
        const profileId = route.params.profileId;
        logger.log('id had', profileId);
        await accountService.getAccountById(profileId);
      }
      catch (error) {
        Pop.error(error);
      }
    }

    async function getVaultsByAccountId() {
      try {
        const profileId = route.params.profileId;
        logger.log('fetching', profileId)
        await vaultsService.getVaultsByAccountId(profileId)
      }
      catch (error) {
        Pop.error(error);
      }
    }

    async function getKeepsByAccountId() {
      try {
        const profileId = route.params.profileId;
        logger.log('fetching keeps', profileId)
        await keepsService.getKeepsByAccountId(profileId)
      }
      catch (error) {
        Pop.error(error);
      }
    }
    // const activeKeep = computed(() => AppState.activeKeep)

    onMounted(() => {
      getAccountById();
      getVaultsByAccountId();
      getKeepsByAccountId();

    })
    return {
      profile: computed(() => AppState.activeAccount),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
      // activeKeep
    }
  }
}
</script>

<style scoped>
.container-fluid {
  background-color: #dcdcdc;
  /* Cooler, more modern gray */
  min-height: 100vh;
  /* Ensure the background covers the full page height */
  padding: 0;

  margin: 0;
}

.creatorImg {
  width: 50%;
  border-radius: 100px;
  object-fit: cover;
  height: 20vh;
}

.card {
  border: 2px solid black;
  border-radius: 16px;
  box-shadow: 3px 3px 10px rgba(42, 6, 134, 0.31);
}

.image {
  max-width: 100px;
}

.banner-img {
  width: 100vw;
  /* Full width of the viewport */
  height: 300px;
  /* Set a fixed height (adjust as needed) */
  object-fit: cover;
  /* Ensure the image covers the banner area without distortion */
  /* display: block; */
  /* Remove inline spacing */
  /* position: relative; */
  /* Ensure it stays in the normal flow or customize as needed */
  margin: 0;

}

/* body {
  margin: 0;
  padding: 0;
} */

.masonry {
  column-count: 3;
  /* Set the number of columns */
  column-gap: 1rem;
  /* Adjust gap between columns */


}

.masonry>div {
  break-inside: avoid;
  /* Prevent the item from breaking between columns */
  margin-bottom: 1rem;
  /* Add some spacing between items */
}

/* For tablets (medium screens), show 2 columns */
@media (max-width: 768px) {
  .masonry {
    column-count: 2;
    /* Reduce to 2 columns */
  }
}

/* For mobile phones (small screens), show 1 column */
@media (max-width: 576px) {
  .masonry {
    column-count: 1;
    /* Reduce to 1 column */
  }
}
</style>

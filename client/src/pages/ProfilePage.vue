<template>
  <section class="container-fluid bg-info">



    <div v-if="profile != null">
      <div class="d-flex justify-content-center">

        <h1>Welcome to {{ profile.name }}'s profile!</h1>


        <div class="">
          <img :src="profile.coverImg" alt="">
        </div>
        <div class="">
          <img class="rounded" :src="profile.picture" alt="" />
        </div>
      </div>
    </div>

    <!-- <div class="row">
      <div class="col-12">
        <p>this is your profile page, I need to try to get vaults page working in time so it looks mid right now</p>
      </div>
    </div> -->

    <div class="row">
      <h3>Keeps</h3>
      <div v-for="keep in keeps" :key="keep.id" class="col-9 col-md-3 m-2 card mb-2 mt-2">
        <KeepComponent :keep="keep" />
      </div>

    </div>
    <div class="row">
      <h3>Vaults</h3>
      <div v-for="vault in vaults" :key="vault.id" class="col-9 col-md-3 m-2 card mb-2 mt-2">
        <VaultComponent :vault="vault" />
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
  /* background-color: green; */
  height: 100vh;
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

img {
  max-width: 100px;
}
</style>

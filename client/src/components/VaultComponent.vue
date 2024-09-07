<template>

    <section class="row d-flex justify-content-center">
        <div>
            <!-- <p class="mb-0 text-center">Views:{{ keep.views }} Kept:{{ keep.kept }}</p> -->
            <!-- <p>render check</p> -->
            <h2 class="text-center">{{ vault.name }}</h2>


            <RouterLink :to="{ name: 'VaultDetails', params: { vaultId: vault.id } }">
                <div>


                    <img :src="vault.img" :alt="vault.name" class="img-fluid">

                </div>
            </RouterLink>


            <p>{{ vault.description }}</p>
        </div>

        <!-- <div class="text-center" v-if="account.id == vault.creatorId">

            <button @click="deleteVault(vaultId)" type="button" class="btn btn-primary">DELETE</button>

        </div> -->

    </section>


</template>


<script>
import { computed } from 'vue';
import { Vault } from '../models/Vault';
import { AppState } from '../AppState';
import { vaultsService } from '../services/VaultsService';
import Pop from "../utils/Pop.js";

export default {
    props: {
        vault: { type: Vault, required: true }
    },
    setup(props) {
        return {


            vaults: computed(() => AppState.accountVaults),
            // vaultId: computed(() => AppState.activeVault),
            account: computed(() => AppState.account),



            // async deleteVault(vaultId) {
            //     try {
            //         const yes = await Pop.confirm()
            //         if (!yes) return
            //         await vaultsService.deleteVault(vaultId)
            //     }
            //     catch (error) {
            //         Pop.error(error);
            //     }
            // }
        }
    }
}
</script>


<style lang="scss" scoped></style>
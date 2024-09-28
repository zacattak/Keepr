<template>

    <section class="row d-flex justify-content-center">
        <div @click="getKeepById(keep.id, vaultKeepId)" class="selectable" type="button" data-bs-toggle="modal"
            data-bs-target="#keepModal">
            <p class="mb-0 text-center">Views:{{ keep.views }} Kept:{{ keep.kept }}</p>
            <h2 class="text-center">{{ keep.name }}</h2>
            <img :src="keep.img" :alt="keep.name" class="img-fluid rounded">
        </div>
        <div v-if="keep.creatorId == account.id">

            <button @click="deleteVaultKeep(vaultKeepId)" type="button" class="btn btn-primary">DELETE</button>
        </div>

    </section>



</template>


<script>
import { Keep } from '../models/Keep.js';
import { KeepClone } from '../models/KeepClone.js';
// import { VaultKeep } from '../models/VaultKeep.js';
import { keepsService } from '../services/KeepsService.js';
import { vaultKeepsService } from '../services/VaultKeepsService.js';
import Pop from '../utils/Pop.js';
import { computed } from 'vue';
import { AppState } from '../AppState';
// import { useRouter } from 'vue-router';
export default {
    props: {
        keep: { type: Keep, required: true },
        vaultKeepId: { type: Number }
    },
    setup() {

        // const router = useRouter();

        return {
            // vaultKeeps: computed(() => AppState.vaultKeeps),

            account: computed(() => AppState.account),




            async getKeepById(keepId, vaultKeepId) {
                try {
                    await keepsService.getKeepById(keepId);
                    if (vaultKeepId != undefined) {
                        vaultKeepsService.getVaultKeepById(vaultKeepId)
                    }
                    else {
                        vaultKeepsService.getVaultKeepById(0)
                    }
                }
                catch (error) {
                    Pop.error(error);
                }
            },


            async deleteVaultKeep(vaultKeepId) {
                try {
                    const yes = await Pop.confirm()
                    if (!yes) return

                    await vaultKeepsService.deleteVaultKeep(vaultKeepId)
                    // router.push({ name: 'Home' })
                }
                catch (error) {
                    Pop.error(error);
                }
            },

        }
    }
}
</script>


<style lang="scss" scoped>
.img {
    width: 100%;
    object-fit: cover;
    height: 40vh;
}
</style>
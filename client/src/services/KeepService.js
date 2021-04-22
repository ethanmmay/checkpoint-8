/* eslint-disable no-undef */
import { AppState } from '../AppState'
import { Keep } from '../models/Keep'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { profileService } from './ProfileService'

class KeepService {
  async getKeeps() {
    try {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data.map(k => new Keep(k))
    } catch (err) {
      logger.error('Couldnt load Keeps', err)
    }
  }

  async getKeepsByVaultId(vaultId) {
    try {
      const res = await api.get(`api/vaults/${vaultId}/keeps`)
      AppState.keepsByVault = res.data.map(k => new Keep(k))
    } catch (err) {
      logger.error('Couldnt load Keeps', err)
    }
  }

  async getPrivateKeeps(vaultId) {
    try {
      const res = await api.get(`api/vaults/${vaultId}/privatekeeps`)
      AppState.keepsByPrivateVault = res.data.map(k => new Keep(k))
    } catch (err) {
      logger.error('Couldnt load Keeps', err)
    }
  }

  setActiveKeep(keep) {
    try {
      AppState.activeKeep = keep
    } catch (error) {
      logger.error('Cant set active keep', error)
    }
  }

  async createKeep() {
    try {
      Swal.fire({
        title: 'New Keep',
        html: '<input type="text" id="name" class="swal2-input" placeholder="Enter Keep Name.. "><textarea type="text" id="img" class="swal2-input pt-2" placeholder="Image Url..."></textarea><textarea type="text" id="description" class="swal2-input pt-2" placeholder="Describe the keep..."></textarea>',
        confirmButtonText: 'Post',
        focusConfirm: false,
        preConfirm: () => {
          const name = Swal.getPopup().querySelector('#name').value
          const img = Swal.getPopup().querySelector('#img').value
          const description = Swal.getPopup().querySelector('#description').value
          if (!name || !img || !description) {
            Swal.showValidationMessage('Please enter a name, description and image URL.')
          }
          return { name: name, img: img, description: description }
        }
      }).then(async(result) => {
        const newKeep = {
          name: result.value.name,
          img: result.value.img,
          description: result.value.description,
          creatorId: AppState.account.id
        }
        await api.post('api/keeps', newKeep)
        await profileService.getProfileKeeps(AppState.account.id)
      })
    } catch (err) {
      logger.error('Couldnt create Keep', err)
    }
  }

  async deleteKeep(keepId) {
    try {
      if (window.confirm('Are you sure you want to delete your Keep?')) {
        await api.delete('api/keeps/' + keepId)
        this.getKeeps()
        document.getElementById('keepModalClose').click()
      }
    } catch (error) {
      logger.log(error)
    }
  }
}

export const keepService = new KeepService()

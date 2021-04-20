export class Keep {
  constructor(data = {}) {
    this.id = data.id || ''
    this.name = data.name || ''
    this.description = data.description || ''
    this.img = data.img || '//placehold.it/400x400'
    this.views = data.views || 0
    this.shares = data.shares || 0
    this.keeps = data.keeps || 0
    this.creator = data.creator || { name: 'unknown', picture: '//placehold.it/20x20' }
  }
}
